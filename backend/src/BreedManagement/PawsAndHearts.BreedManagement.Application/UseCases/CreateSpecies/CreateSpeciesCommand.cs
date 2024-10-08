using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;