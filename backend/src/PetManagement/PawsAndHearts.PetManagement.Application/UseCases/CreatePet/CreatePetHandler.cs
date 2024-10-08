using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.BreedManagement.Contracts;
using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Extensions;
using PawsAndHearts.PetManagement.Application.Interfaces;
using PawsAndHearts.PetManagement.Domain.Entities;
using PawsAndHearts.PetManagement.Domain.Enums;
using PawsAndHearts.PetManagement.Domain.ValueObjects;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;
using PawsAndHearts.SharedKernel.ValueObjects.Ids;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreatePet;

public class CreatePetHandler : ICommandHandler<Guid, CreatePetCommand>
{
    private readonly IVolunteersRepository _volunteersRepository;
    private readonly IBreedManagementContract _breedManagementContract;
    private readonly IPetManagementUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetCommand> _validator;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IVolunteersRepository volunteersRepository,
        IBreedManagementContract breedManagementContract,
        IPetManagementUnitOfWork unitOfWork,
        IValidator<CreatePetCommand> validator,
        ILogger<CreatePetHandler> logger)
    {
        _volunteersRepository = volunteersRepository;
        _breedManagementContract = breedManagementContract;
        _unitOfWork = unitOfWork;
        _validator = validator;
        _logger = logger;
    }

    public async Task<Result<Guid, ErrorList>> Handle(
        CreatePetCommand command,
        CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToErrorList();
        
        var volunteerResult = await _volunteersRepository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();
        
        var speciesResult = await _breedManagementContract
            .GetSpeciesById(command.SpeciesId, cancellationToken);

        if (speciesResult.IsFailure)
            return speciesResult.Error;

        var breedResult = await _breedManagementContract
            .GetBreedBySpecies(command.SpeciesId, command.BreedId, cancellationToken);

        if (breedResult.IsFailure)
            return breedResult.Error;

        var petId = PetId.NewId();

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

        var creationDate = CreationDate.Create(command.BirthDate).Value;

        var requisites = command.Requisites.Select(f =>
                Requisite.Create(f.Name, f.Description).Value).ToList();

        var petIdentity = new PetIdentity(command.SpeciesId, command.BreedId);
            
        var pet = new Pet(
            petId,
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

        volunteerResult.Value.AddPet(pet);

        await _unitOfWork.SaveChanges(cancellationToken);
        
        _logger.LogInformation("Pet was created with id {petId}", (Guid)petId);
        
        return (Guid)pet.Id;
    }
}