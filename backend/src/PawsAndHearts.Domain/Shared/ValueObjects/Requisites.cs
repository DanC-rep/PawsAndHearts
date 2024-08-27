namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record Requisites
{
    private Requisites()
    {
    }
    
    public Requisites(IEnumerable<Requisite>? requisites)
    {
        Value = requisites?.ToList();
    }
    
    public IReadOnlyList<Requisite>? Value { get; }
}