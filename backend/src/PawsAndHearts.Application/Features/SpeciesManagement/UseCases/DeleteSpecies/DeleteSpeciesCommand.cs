using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteSpecies;

public record DeleteSpeciesCommand(Guid SpeciesId) : ICommand;