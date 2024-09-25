using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.DeleteBreed;

public record DeleteBreedCommand(Guid SpeciesId, Guid BreedId) : ICommand;