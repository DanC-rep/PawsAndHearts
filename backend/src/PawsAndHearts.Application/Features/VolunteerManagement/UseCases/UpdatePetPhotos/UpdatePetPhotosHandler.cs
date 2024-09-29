using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.FileProvider;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Volunteer.ValueObjects;
using FileInfo = PawsAndHearts.Application.FileProvider.FileInfo;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetPhotos;

public class UpdatePetPhotosHandler : ICommandHandler<FilePathList, UpdatePetPhotosCommand>
{
    private const string BUCKET_NAME = "photos";
    
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly IValidator<UpdatePetPhotosCommand> _validator;
    private readonly ILogger<UpdatePetPhotosHandler> _logger;

    public UpdatePetPhotosHandler(
        IVolunteersRepository volunteersRepository, 
        IFileProvider fileProvider, 
        IUnitOfWork unitOfWork, 
        IMessageQueue<IEnumerable<FileInfo>> messageQueue, 
        IValidator<UpdatePetPhotosCommand> validator, 
        ILogger<UpdatePetPhotosHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<FilePathList, ErrorList>> Handle(
        UpdatePetPhotosCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petResult = volunteerResult.Value.GetPetById(command.PetId);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();

            List<UploadFileData> fileDatas = [];

            foreach (var file in command.FileDtos)
            {
                var extension = Path.GetExtension(file.FileName);
                
                var filePathResult = FilePath.Create(Guid.NewGuid(), extension);

                if (filePathResult.IsFailure)
                    return filePathResult.Error.ToErrorList();

                var fileData = new UploadFileData(file.Content, new FileInfo(filePathResult.Value, BUCKET_NAME));

                fileDatas.Add(fileData);
            }
            
            var petPhotos = fileDatas
                .Select(f => f.Info.FilePath)
                .Select(f => PetPhoto.Create(f, false).Value)
                .ToList();
            
            var petPreviousPhotos = petResult.Value.PetPhotos!
                .Select(p => new FileInfo(p.Path, BUCKET_NAME)).ToList();

            foreach (var photo in petPreviousPhotos)
            {
                var deleteResult = await _fileProvider.Delete(photo, cancellationToken);

                if (deleteResult.IsFailure)
                    return deleteResult.Error.ToErrorList();
            }

            petResult.Value.AddPhotos(petPhotos);

            await _unitOfWork.SaveChanges(cancellationToken);

            var uploadResult = await _fileProvider.UploadFiles(fileDatas, cancellationToken);

            if (uploadResult.IsFailure)
            {
                await _messageQueue.WriteAsync(fileDatas.Select(f => f.Info), cancellationToken);
                return uploadResult.Error.ToErrorList();
            }

            transaction.Commit();

            _logger.LogInformation("Files was updated for pet {petId}", command.PetId);

            return uploadResult.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to update files for pet {petId}", command.PetId);
            
            transaction.Rollback();

            return Error.Failure("pet.update.files", "Can not update files for pet")
                .ToErrorList();
        }
    }
}