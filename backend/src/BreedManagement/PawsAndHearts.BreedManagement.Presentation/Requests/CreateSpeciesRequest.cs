using PawsAndHearts.BreedManagement.Application.UseCases.CreateSpecies;

namespace PawsAndHearts.BreedManagement.Presentation.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => 
        new (Name);
}