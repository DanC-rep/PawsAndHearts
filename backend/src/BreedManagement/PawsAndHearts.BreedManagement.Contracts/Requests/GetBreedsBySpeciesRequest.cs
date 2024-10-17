namespace PawsAndHearts.BreedManagement.Contracts.Requests;

public record GetBreedsBySpeciesRequest(Guid SpeciesId, string? SortDirection);