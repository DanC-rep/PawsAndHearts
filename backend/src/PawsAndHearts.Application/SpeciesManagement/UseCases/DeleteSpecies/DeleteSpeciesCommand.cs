using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;