using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Dtos;

namespace PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

public record CreatePetRequest(
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
    IEnumerable<RequisiteDto> Requisites);