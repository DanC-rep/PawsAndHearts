using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PawsAndHearts.BreedManagement.Contracts;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Enums;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Domain.Entities;
using PawsAndHearts.PetManagement.Domain.Enums;
using PawsAndHearts.PetManagement.Domain.ValueObjects;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePet;

public class UpdatePetHandler : ICommandHandler<Guid, UpdatePetCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBreedManagementContract _breedManagementContract;
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly ILogger<UpdatePetHandler> _logger;
    private readonly IValidator<UpdatePetCommand> _validator;

    public UpdatePetHandler(
        [FromKeyedServices(Modules.PetManagement)] IUnitOfWork unitOfWork, 
        IBreedManagementContract breedManagementContract,
        IVolunteersRepository volunteersRepository,
        ILogger<UpdatePetHandler> logger,
        IValidator<UpdatePetCommand> validator)
    {
        _unitOfWork = unitOfWork;
        _breedManagementContract = breedManagementContract;
        _volunteersRepository = volunteersRepository;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        UpdatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();

        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

        var petResult = volunteerResult.Value.GetPetById(command.PetId);

        if (petResult.IsFailure)
            return petResult.Error.ToErrorList();

        var speciesResult = await _breedManagementContract
            .GetSpeciesById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error;

        var breedResult = await _breedManagementContract
                .GetBreedBySpecies(command.BreedId, command.SpeciesId, cancellationToken);
        
        if (breedResult.IsFailure)
            return breedResult.Error;

        if (!Enum.TryParse(command.HelpStatus, out HelpStatus helpStatus))
            return Errors.General.ValueIsInvalid("help status").ToErrorList();

        var color = Color.Create(command.Color).Value;

        var address = Address.Create(
            command.Address.City,
            command.Address.Street,
            command.Address.House,
            command.Address.Flat).Value;
        
        var petMetrics = PetMetrics.Create(
            command.PetMetrics.Height,
            command.PetMetrics.Weight).Value;

        var phoneNumber = PhoneNumber.Create(command.PhoneNumber).Value;

        var birthDate = BirthDate.Create(command.BirthDate).Value;

        var creationDate = CreationDate.Create(command.CreationDate).Value;

        var requisites = command.Requisites.Select(f =>
            Requisite.Create(f.Name, f.Description).Value).ToList();

        var petIdentity = new PetIdentity(command.SpeciesId, command.BreedId);

        var updatedPet = new Pet(
            petResult.Value.Id,
            command.Name,
            command.Description,
            petIdentity,
            color,
            command.HealthInfo,
            address,
            petMetrics,
            phoneNumber,
            command.IsNeutered,
            birthDate,
            command.IsVaccinated,
            helpStatus,
            creationDate,
            requisites);

        volunteerResult.Value.UpdatePet(updatedPet);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet was updated with id {petId}", command.PetId);

        return command.PetId;
    }
}