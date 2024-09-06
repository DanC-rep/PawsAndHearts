using PawsAndHearts.Application.Dto;
using PawsAndHearts.Domain.Volunteer.Enums;

namespace PawsAndHearts.API.Contracts;

public record CreatePetRequest(
    string Name,
    string Description,
    string Color,
    string HealthInfo,
    AddressDto Address,
    PetMetricsDto PetMetrics,
    string PhoneNumber,
    bool IsNeutered,
    DateTime BirthDate,
    bool IsVaccinated,
    HelpStatus HelpStatus,
    DateTime CreationDate,
    IEnumerable<RequisiteDto> Requisites);