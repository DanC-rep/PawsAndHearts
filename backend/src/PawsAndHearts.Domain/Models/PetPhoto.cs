namespace PawsAndHearts.Domain.Models;

public class PetPhoto
{
    public Guid Id { get; private set; }

    public string Path { get; private set; } = default!;
    
    public bool IsMain { get; private set; }

    public Guid PetId { get; private set; }
}