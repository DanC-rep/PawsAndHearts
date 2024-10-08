using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePet;

public record UpdatePetCommand(
    Guid VolunteerId,
    Guid PetId,
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