using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreatePet;

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
    IEnumerable<RequisiteDto> Requisites) : ICommand
{
    public static CreatePetCommand Create(Guid volunteerId, CreatePetRequest request) =>
        new(
            volunteerId,
            request.Name,
            request.Description,
            request.Color,
            request.HealthInfo,
            request.SpeciesId,
            request.BreedId,
            request.Address,
            request.PetMetrics,
            request.PhoneNumber,
            request.IsNeutered,
            request.BirthDate,
            request.IsVaccinated,
            request.HelpStatus,
            request.CreationDate,
            request.Requisites);
}