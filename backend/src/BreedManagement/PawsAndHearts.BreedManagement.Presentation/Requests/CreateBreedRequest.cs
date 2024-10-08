using PawsAndHearts.BreedManagement.Application.UseCases.CreateBreed;

namespace PawsAndHearts.BreedManagement.Presentation.Requests;

public record CreateBreedRequest(string Name)
{
    public CreateBreedCommand ToCommand(Guid speciesId) =>
        new(speciesId, Name);
}