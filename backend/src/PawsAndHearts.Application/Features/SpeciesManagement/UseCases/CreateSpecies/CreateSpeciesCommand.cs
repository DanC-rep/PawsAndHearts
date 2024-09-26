using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;