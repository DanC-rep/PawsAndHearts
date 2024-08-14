using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Models;

public class PetPhoto : Entity<BaseId>
{
    private PetPhoto(BaseId id) : base(id)
    {
    }

    public string Path { get; private set; } = default!;
    
    public bool IsMain { get; private set; }

    public BaseId PetId { get; private set; }
}