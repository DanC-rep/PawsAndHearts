using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateBreed;

public record CreateBreedCommand(Guid SpeciesId, string Name) : ICommand;