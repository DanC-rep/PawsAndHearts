using PawsAndHearts.Domain.Enums;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Pet : Entity<PetId>
{
    private Pet(PetId id) : base(id)
    {
    }

    public string Name { get; private set; } = default!;

    public string Species { get; private set; } = default!;

    public string Description { get; private set; } = default!;

    public string Breed { get; private set; } = default!;

    public string Color { get; private set; } = default!;

    public string HealthInfo { get; private set; } = default!;

    public Address Address { get; private set; }

    public double Weight { get; private set; }
    
    public double Height { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }
    
    public bool IsNeutered { get; private set; }
    
    public BirthDate BirthDate { get; private set; }
    
    public bool IsVaccinated { get; private set; }
    
    public HelpStatus HelpStatus { get; private set; }
    
    public CreationDate CreationDate { get; private set; }

    public PetDetails PetDetails { get; private set; }
}