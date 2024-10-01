﻿using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.DeletePetSoft;

public class DeletePetSoftHandler : ICommandHandler<Guid, DeletePetSoftCommand>
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePetSoftCommand> _logger;

    public DeletePetSoftHandler(
        IVolunteersRepository repository,
        IUnitOfWork unitOfWork,
        ILogger<DeletePetSoftCommand> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        DeletePetSoftCommand command, 
        CancellationToken cancellationToken)
    {
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);

        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        petResult.Value.Delete();

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet was soft deleted with id {petId}", command.PetId);

        return (Guid)petResult.Value.Id;
    }
}