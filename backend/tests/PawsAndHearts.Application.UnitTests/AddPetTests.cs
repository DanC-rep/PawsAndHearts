using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Moq;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Application.VolunteerManagement.UseCases.CreatePet;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.Enums;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Species.Entities;
using PawsAndHearts.Domain.Volunteer.Entities;
using PawsAndHearts.Domain.Volunteer.Enums;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Application.UnitTests;

public class AddPetTests
{
    private readonly Mock<IVolunteersRepository> _volunteersRepositoryMock = new();
    private readonly Mock<ISpeciesRepository> _speciesRepositoryMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IValidator<CreatePetCommand>> _validatorMock = new();
    private readonly Mock<ILogger<CreatePetHandler>> _loggerMock = new();

    [Fact]
    public async Task Handle_Should_Create_Pet()
    {
        // act
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = CreateVolunteerWithPets(0);
        var species = CreateSpeciesWithBreed(1);

        var command = new CreatePetCommand(
            volunteer.Id,
            "test",
            "test",
            "test",
            "test",
            species.Id,
            species.Breeds.First().Id,
            new AddressDto("123", "123", "123"),
            new PetMetricsDto(10, 10),
            "89205598871",
            false,
            DateTime.Now,
            true,
            "FoundHome",
            DateTime.Now,
            new List<RequisiteDto>()
            {
                new RequisiteDto("name", "description")
            });

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(new ValidationResult());

        _volunteersRepositoryMock.Setup(v => v.GetById(volunteer.Id, cancellationToken))
            .ReturnsAsync(volunteer);

        _speciesRepositoryMock.Setup(s => s.GetById(species.Id, cancellationToken))
            .ReturnsAsync(species);

        _unitOfWorkMock.Setup(u => u.SaveChanges(cancellationToken))
            .Returns(Task.CompletedTask);

        var handler = new CreatePetHandler(
            _volunteersRepositoryMock.Object,
            _speciesRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object,
            _loggerMock.Object);
        
        // arrange
        var result = await handler.Handle(command, cancellationToken);
        
        // assert
        result.IsSuccess.Should().BeTrue();
        volunteer.Pets.Should().ContainSingle();
    }

    [Fact]
    public async Task Handle_Should_Return_Validation_Errors()
    {
        // act
        var cancellationToken = new CancellationTokenSource().Token;
        var volunteer = CreateVolunteerWithPets(0);

        var invalidNumber = "1111";

        var command = new CreatePetCommand(
            volunteer.Id,
            "test",
            "test",
            "test",
            "test",
            Guid.Empty,
            Guid.Empty,
            new AddressDto("123", "123", "123"),
            new PetMetricsDto(10, 10),
            invalidNumber,
            false,
            DateTime.Now,
            true,
            "FoundHome",
            DateTime.Now,
            new List<RequisiteDto>()
            {
                new RequisiteDto("name", "description")
            });

        var errorValidation = Errors.General.ValueIsInvalid("phone number").Serialize();

        var validationFailures = new List<ValidationFailure>
        {
            new("phone number", errorValidation)
        };

        var validationResult = new ValidationResult(validationFailures);

        _validatorMock.Setup(v => v.ValidateAsync(command, cancellationToken))
            .ReturnsAsync(validationResult);
        
        var handler = new CreatePetHandler(
            _volunteersRepositoryMock.Object,
            _speciesRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _validatorMock.Object,
            _loggerMock.Object);
        
        // arrange
        var result = await handler.Handle(command, cancellationToken);
        
        // assert
        result.IsFailure.Should().BeTrue();

        var error = result.Error.First();
        error.Type.Should().Be(ErrorType.Validation);
        error.Code.Should().Be("value.is.invalid");
        error.InvalidField.Should().Be("phone number");
    }

    private Volunteer CreateVolunteerWithPets(int petsCount)
    {
        var petId = PetId.NewId();
        var fullName = FullName.Create("123", "123", "123").Value;
        var experience = Experience.Create(1).Value;
        var phoneNumber = PhoneNumber.Create("89205598871").Value;
        var petIdentity = new PetIdentity(SpeciesId.Empty(), Guid.Empty);
        var color = Color.Create("123").Value;
        var address = Address.Create("123", "123", "123", "213").Value;
        var petMetrics = PetMetrics.Create(123, 213).Value;
        var birthDate = BirthDate.Create(new DateOnly(2024, 01, 01)).Value;
        var creationDate = CreationDate.Create(new DateOnly(2024, 02, 01)).Value;

        var socialNetworks = new ValueObjectList<SocialNetwork>(
            [SocialNetwork.Create("123", "123").Value]);

        var requisites = new ValueObjectList<Requisite>(
            [Requisite.Create("123", "123").Value]);

        var volunteer = new Volunteer(
            VolunteerId.NewId(),
            fullName,
            experience,
            phoneNumber,
            socialNetworks,
            requisites);

        for (int i = 0; i < petsCount; i++)
        {
            var pet = new Pet(
                petId,
                "123",
                "123",
                petIdentity,
                color,
                "norm",
                address,
                petMetrics,
                phoneNumber,
                true,
                birthDate,
                true,
                HelpStatus.FoundHome,
                creationDate,
                requisites);

            volunteer.AddPet(pet);
        }

        return volunteer;
    }

    private Species CreateSpeciesWithBreed(int breedsCount)
    {
        var speciesId = SpeciesId.NewId();
        var speciesName = "example";
        
        var species = new Species(speciesId, speciesName);

        for (int i = 0; i < breedsCount; i++)
        {
            var breedId = BreedId.NewId();
            var breedName = $"breed {i + 1}";

            var breed = new Breed(breedId, breedName, speciesId);

            species.AddBreed(breed);
        }

        return species;
    }
}