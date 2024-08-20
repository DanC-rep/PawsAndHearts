using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Domain.Volunteer.Entities;

public class Volunteer : Shared.Entity<VolunteerId>
{
    private Volunteer(VolunteerId id) : base(id)
    {
    }
    
    private Volunteer(
        VolunteerId id, 
        FullName fullName, 
        int experience, 
        VolunteerPetsMetrics volunteerPetsMetrics,
        PhoneNumber phoneNumber, 
        SocialNetworks socialNetworks,
        Requisites requisites) : base(id)
    {
        FullName = fullName;
        Experience = experience;
        VolunteerPetsMetrics = volunteerPetsMetrics;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        Requisites = requisites;
    }

    public FullName FullName { get; private set; }
    
    public int Experience { get; private set; }
    
    public VolunteerPetsMetrics VolunteerPetsMetrics { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public SocialNetworks SocialNetworks { get; private set; }
    
    public Requisites Requisites { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;

    public static Result<Volunteer, Error> Create(
        VolunteerId id, 
        FullName fullName, 
        int experience, 
        VolunteerPetsMetrics volunteerPetsMetrics,
        PhoneNumber phoneNumber, 
        SocialNetworks socialNetworks,
        Requisites requisites)
    {
        return new Volunteer(
            id, 
            fullName, 
            experience, 
            volunteerPetsMetrics,
            phoneNumber, 
            socialNetworks,
            requisites);
    }
}