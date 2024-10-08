using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.UpdatePet;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdatePetRequest(
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
    IEnumerable<RequisiteDto> Requisites)
{
    public UpdatePetCommand ToCommand(Guid volunteerId, Guid petId) =>
        new(
            volunteerId,
            petId,
            Name,
            Description,
            Color,
            HealthInfo,
            SpeciesId,
            BreedId,
            Address,
            PetMetrics,
            PhoneNumber,
            IsNeutered,
            BirthDate,
            IsVaccinated,
            HelpStatus,
            CreationDate,
            Requisites);
}