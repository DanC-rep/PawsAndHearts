using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Requisite : Entity<RequisiteId>
{
    private Requisite(RequisiteId id) : base(id)
    {
    }

    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public PetId PetId { get; private set; }

    public VolunteerId VolunteerId { get; private set; }
}