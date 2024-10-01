﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Volunteer.ValueObjects;
using FileInfo = PawsAndHearts.Application.FileProvider.FileInfo;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetForce;

public class DeletePetForceHandler : ICommandHandler<Guid, DeletePetForceCommand>
{
    private const string BUCKET_NAME = "photos";
    
    private readonly IVolunteersRepository _volunteersVolunteersRepository;
    private readonly IPetRepository _petRepository;
    private readonly IFileProvider _fileProvider;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMessageQueue<IEnumerable<FileInfo>> _messageQueue;
    private readonly ILogger<DeletePetForceHandler> _logger;

    public DeletePetForceHandler(
        IVolunteersRepository volunteersRepository,
        IPetRepository petRepository,
        IFileProvider fileProvider,
        IUnitOfWork unitOfWork,
        IMessageQueue<IEnumerable<FileInfo>> messageQueue,
        ILogger<DeletePetForceHandler> logger)
    {
        _volunteersVolunteersRepository = volunteersRepository;
        _petRepository = petRepository;
        _fileProvider = fileProvider;
        _unitOfWork = unitOfWork;
        _messageQueue = messageQueue;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeletePetForceCommand command,
        CancellationToken cancellationToken = default)
    {
        var transaction = await _unitOfWork.BeginTransaction(cancellationToken);

        try
        {
            var volunteerResult = await _volunteersVolunteersRepository.GetById(command.VolunteerId, cancellationToken);

            if (volunteerResult.IsFailure)
                return volunteerResult.Error.ToErrorList();

            var petResult = volunteerResult.Value.GetPetById(command.PetId);

            if (petResult.IsFailure)
                return petResult.Error.ToErrorList();
            
            var petPreviousPhotosInfo = (petResult.Value.PetPhotos ?? new List<PetPhoto>())
                .Select(p => new FileInfo(p.Path, BUCKET_NAME)).ToList();
            
            _petRepository.Delete(petResult.Value);

            await _unitOfWork.SaveChanges(cancellationToken);
            
            var deleteResult = await _fileProvider.DeleteFiles(petPreviousPhotosInfo, cancellationToken);

            if (deleteResult.IsFailure)
            {
                await _messageQueue.WriteAsync(petPreviousPhotosInfo, cancellationToken);
                return deleteResult.Error.ToErrorList();
            }
            
            transaction.Commit();

            _logger.LogInformation("Pet was force deleted with id {petId}", command.PetId);

            return command.PetId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Fail to delete pet with id {petId}", command.PetId);
            
            transaction.Rollback();

            return Error.Failure("pet.delete", "Can not delete pet")
                .ToErrorList();
        }
    }
}