using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateSpecies;

namespace PawsAndHearts.API.Controllers.Species.Requests;

public record CreateSpeciesRequest(string Name)
{
    public CreateSpeciesCommand ToCommand() => 
        new (Name);
}