namespace PawsAndHearts.Core.Dtos;

public class PetDto
{
    public Guid Id { get; init; }

    public string Name { get; init; } = default!;

    public string Description { get; init; } = default!;
    
    public Guid SpeciesId { get; init; }
    
    public Guid BreedId { get; init; }
    
    public Guid VolunteerId { get; init; }
    
    public string Color { get; init; } = default!;

    public string HealthInfo { get; init; } = default!;

    public string City { get; init; } = default!;

    public string Street { get; init; } = default!;

    public string House { get; init; } = default!;
    
    public string? Flat { get; init; }
    
    public double Height { get; init; }
    
    public double Weight { get; init; }

    public string PhoneNumber { get; init; } = default!;
    
    public bool IsNeutered { get; init; }
    
    public DateTime BirthDate { get; init; }
    
    public bool IsVaccinated { get; init; }

    public string HelpStatus { get; init; } = default!;
    
    public DateTime CreationDate { get; init; }
    
    public int Position { get; init; }

    public IEnumerable<RequisiteDto> Requisites { get; init; } = [];
    
    public IEnumerable<PetPhotoDto>? PetPhotos { get; init; } = [];
}