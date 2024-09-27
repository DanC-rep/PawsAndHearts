using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateBreed;

public record CreateBreedCommand(Guid SpeciesId, string Name) : ICommand;