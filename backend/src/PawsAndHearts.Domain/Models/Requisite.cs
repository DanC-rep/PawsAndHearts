namespace PawsAndHearts.Domain.Models;

public class Requisite
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public Guid PetId { get; private set; }

    public Guid VolunteerId { get; private set; }
}