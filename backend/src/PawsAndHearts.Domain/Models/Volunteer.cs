using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Domain.Models;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
    }
    
    private Volunteer(
        VolunteerId id, 
        FullName fullName, 
        int experience, 
        int petsFoundHome,
        int petsLookingForHome, 
        int petsBeingTreated, 
        PhoneNumber phoneNumber, 
        VolunteerDetails volunteerDetails) : base(id)
    {
        FullName = fullName;
        Experience = experience;
        PetsFoundHome = petsFoundHome;
        PetsLookingForHome = petsLookingForHome;
        PetsBeingTreated = petsBeingTreated;
        PhoneNumber = phoneNumber;
        VolunteerDetails = volunteerDetails;
    }

    public FullName FullName { get; private set; }
    
    public int Experience { get; private set; }
    
    public int PetsFoundHome { get; private set; }
    
    public int PetsLookingForHome { get; private set; }
    
    public int PetsBeingTreated { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public VolunteerDetails VolunteerDetails { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer> Create(
        VolunteerId id, 
        FullName fullName, 
        int experience, 
        int petsFoundHome,
        int petsLookingForHone, 
        int petsBeingTreated, 
        PhoneNumber phoneNumber, 
        VolunteerDetails volunteerDetails)
    {
        return new Volunteer(
            id, 
            fullName, 
            experience, 
            petsFoundHome, 
            petsLookingForHone, 
            petsBeingTreated,
            phoneNumber, 
            volunteerDetails);
    }
}