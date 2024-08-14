using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Models;

public class Requisite : Entity<BaseId>
{
    private Requisite(BaseId id) : base(id)
    {
    }

    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public BaseId PetId { get; private set; }

    public BaseId VolunteerId { get; private set; }
}