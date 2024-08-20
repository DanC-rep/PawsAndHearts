namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record PetPhotos
{
    private PetPhotos()
    {
    }
    
    public PetPhotos(IEnumerable<PetPhoto>? petPhotos)
    {
        _value = petPhotos?.ToList();
    }
    
    private readonly List<PetPhoto>? _value;

    public IReadOnlyList<PetPhoto>? Value => _value;
}