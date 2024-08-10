using PawsAndHearts.Domain.Enums;

namespace PawsAndHearts.Domain.Models;

public class Pet
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public string Species { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public string Breed { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    public string HealthInfo { get; private set; } = default!;

    public string Address { get; private set; } = default!;

    public double Weight { get; private set; }
    
    public double Height { get; private set; }

    public string PhoneNumber { get; private set; } = default!;
    
    public bool IsNeutered { get; private set; }
    
    public DateOnly BirthDate { get; private set; }
    
    public bool IsVaccinated { get; private set; }
    
    public HelpStatus HelpStatus { get; private set; }

    private readonly List<Requisite> _requisites = [];

    public IReadOnlyList<Requisite> Requisites => _requisites;
    
    public DateOnly CreationDate { get; private set; }
}