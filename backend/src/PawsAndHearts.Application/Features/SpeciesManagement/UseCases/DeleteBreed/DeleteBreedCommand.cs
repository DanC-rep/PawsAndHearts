using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;