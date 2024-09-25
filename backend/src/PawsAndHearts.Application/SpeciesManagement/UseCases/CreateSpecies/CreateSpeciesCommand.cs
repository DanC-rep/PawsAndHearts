using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.CreateSpecies;

public record CreateSpeciesCommand(string Name) : ICommand;