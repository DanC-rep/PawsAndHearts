using PawsAndHearts.BreedManagement.Contracts.Requests;
using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateBreed;

public record CreateBreedCommand(Guid SpeciesId, string Name) : ICommand
{
    public static CreateBreedCommand Create(Guid speciesId, CreateBreedRequest request) =>
        new(speciesId, request.Name);
}