namespace PawsAndHearts.Domain.Shared.ValueObjects;

public record Requisites
{
    private Requisites()
    {
    }
    
    public Requisites(IEnumerable<Requisite>? requisites)
    {
        _value = requisites?.ToList();
    }
    
    private readonly List<Requisite>? _value;

    public IReadOnlyList<Requisite>? Value => _value;
}