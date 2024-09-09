namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record PetPhotos
{
    private PetPhotos()
    {
    }
    
    public PetPhotos(IEnumerable<PetPhoto>? petPhotos)
    {
        Value = petPhotos?.ToList();
    }

    public IReadOnlyList<PetPhoto>? Value { get; }
}