using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class PetPhoto : Entity<PetPhotoId>
{
    private PetPhoto(PetPhotoId id) : base(id)
    {
    }

    public string Path { get; private set; } = default!;
    
    public bool IsMain { get; private set; }

    public PetId PetId { get; private set; }
}