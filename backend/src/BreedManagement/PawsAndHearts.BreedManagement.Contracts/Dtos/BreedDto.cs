namespace PawsAndHearts.BreedManagement.Contracts.Dtos;

public class BreedDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;
    
    public Guid SpeciesId { get; init; }
}