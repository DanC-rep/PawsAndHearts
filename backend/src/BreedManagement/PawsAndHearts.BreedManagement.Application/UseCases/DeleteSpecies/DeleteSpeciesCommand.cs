using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;