using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateBreed;

namespace PawsAndHearts.API.Controllers.Species.Requests;

public record CreateBreedRequest(string Name)
{
    public CreateBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);
}