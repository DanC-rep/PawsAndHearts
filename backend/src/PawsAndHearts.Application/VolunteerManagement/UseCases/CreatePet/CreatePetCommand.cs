using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.CreatePet;

public record CreatePetCommand(
    Guid VolunteerId,
    string Name,
    string Description,
    string Color,
    string HealthInfo,
    Guid SpeciesId,
    Guid BreedId,
    AddressDto Address,
    PetMetricsDto PetMetrics,
    string PhoneNumber,
    bool IsNeutered,
    DateTime BirthDate,
    bool IsVaccinated,
    string HelpStatus,
    DateTime CreationDate,
    IEnumerable<RequisiteDto> Requisites) : ICommand;