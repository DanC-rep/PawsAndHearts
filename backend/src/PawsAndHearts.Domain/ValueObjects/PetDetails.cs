namespace PawsAndHearts.Domain.ValueObjects;

public record PetDetails
{
    private readonly List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites;
    
    private readonly List<PetPhoto> _photos = [];

    public IReadOnlyList<PetPhoto> Photos => _photos;
}