using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PawsAndHearts.Application.Extensions;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Entities;
using PawsAndHearts.Domain.Volunteer.Enums;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.CreatePet;

public class CreatePetHandler
{
    private readonly IVolunteersRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<CreatePetCommand> _validator;
    private readonly ILogger<CreatePetHandler> _logger;

    public CreatePetHandler(
        IVolunteersRepository repository,
        IUnitOfWork unitOfWork,
        IValidator<CreatePetCommand> validator,
        ILogger<CreatePetHandler> logger)
    {
        _repository = repository;
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
        
        var volunteerResult = await _repository.GetById(command.VolunteerId, cancellationToken);

        if (volunteerResult.IsFailure)
            return volunteerResult.Error.ToErrorList();

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

        var birthDate = BirthDate.Create(DateOnly.FromDateTime(command.BirthDate)).Value;

        var creationDate = CreationDate.Create(DateOnly.FromDateTime(command.CreationDate)).Value;

        var requisites = command.Requisites.Select(f =>
                Requisite.Create(f.Name, f.Description).Value).ToList();

        var petIdentity = new PetIdentity(Guid.Empty, Guid.Empty);
            
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