using PawsAndHearts.Core.Abstractions;

namespace PawsAndHearts.BreedManagement.Application.UseCases.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;