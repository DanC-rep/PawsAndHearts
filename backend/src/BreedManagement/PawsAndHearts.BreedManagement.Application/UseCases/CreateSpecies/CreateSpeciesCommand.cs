using PawsAndHearts.BreedManagement.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand
{
    public static CreateSpeciesCommand Create(CreateSpeciesRequest request) =>
        new(request.Name);
}