using PawsAndHearts.Domain.Models;

namespace PawsAndHearts.Domain.ValueObjects;

public record PetPhotoId
{
    private PetPhotoId(Guid value)
    {
        Value = value;
    }
    
    public Guid Value { get; }

    public static PetPhotoId NewId() => new(Guid.NewGuid());

    public static PetPhotoId Empty() => new(Guid.Empty);

    public static PetPhotoId Create(Guid id) => new(id);
}