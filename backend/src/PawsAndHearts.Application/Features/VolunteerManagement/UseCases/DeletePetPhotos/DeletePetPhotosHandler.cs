using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using FileInfo = PawsAndHearts.Application.FileProvider.FileInfo;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetPhotos;

public class DeletePetPhotosHandler : ICommandHandler<DeletePetPhotosCommand>
{
    private const string BUCKET_NAME = "photos";

    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ILogger<DeletePetPhotosHandler> _logger;

    public DeletePetPhotosHandler(
        IVolunteersRepository volunteersRepository,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        ILogger<DeletePetPhotosHandler> logger, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _logger = logger;
        _messageQueue = messageQueue;
    }

    public async Task<UnitResult<ErrorList>> Handle(
        DeletePetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);
        
        try
        {
            var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petResult = volunteerResult.Value.GetPetById(command.PetId);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();
        
            var petPreviousPhotosInfo = petResult.Value.PetPhotos!
                .Select(p => new FileInfo(p.Path, BUCKET_NAME)).ToList();
            
            petResult.Value.DeletePhotos();
            
            await _unitOfWork.SaveChanges(cancellationToken);

            var deleteResult = await _fileProvider.DeleteFiles(petPreviousPhotosInfo, cancellationToken);

            if (deleteResult.IsFailure)
            {
                await _messageQueue.WriteAsync(petPreviousPhotosInfo, cancellationToken);
                return deleteResult.Error.ToErrorList();
            }
            
            transaction.Commit();

            _logger.LogInformation("Files was deleted for pet {petId}", command.PetId);

            return Result.Success<ErrorList>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete files for pet {petId}", command.PetId);
            
            transaction.Rollback();

            return Error.Failure("pet.delete.files", "Can not delete files for pet")
                .ToErrorList();
        }
    }
}