using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.Interfaces;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Shared.ValueObjects.Ids;
using PawsAndHearts.Domain.Volunteer.Enums;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Domain.Volunteer.Entities;

public class Volunteer : Shared.Entity<VolunteerId>, ISoftDeletable
{
    private bool _isDeleted = false;
    
    private Volunteer(VolunteerId id) : base(id)
    {
    }
    
    public Volunteer(
        VolunteerId id, 
        FullName fullName, 
        Experience experience,
        PhoneNumber phoneNumber, 
        SocialNetworks socialNetworks,
        Requisites requisites) : base(id)
    {
        FullName = fullName;
        Experience = experience;
        PhoneNumber = phoneNumber;
        SocialNetworks = socialNetworks;
        Requisites = requisites;
    }

    public FullName FullName { get; private set; }
    
    public Experience Experience { get; private set; }

    public PhoneNumber PhoneNumber { get; private set; }

    public SocialNetworks SocialNetworks { get; private set; }
    
    public Requisites Requisites { get; private set; }

    private readonly List<Pet> _pets = [];

    public IReadOnlyList<Pet> Pets => _pets;

    public int GetPetsFoundHome() => _pets.Count(p => p.HelpStatus == HelpStatus.FoundHome);

    public int GetPetsLookingForHome() => _pets.Count(p => p.HelpStatus == HelpStatus.LookingForHome);

    public int GetPetsBeingTreated() => _pets.Count(p => p.HelpStatus == HelpStatus.NeedForHelp);

    public void UpdateMainInfo(FullName fullName, PhoneNumber phoneNumber, Experience experience)
    {
        FullName = fullName;
        PhoneNumber = phoneNumber;
        Experience = experience;
    }

    public void UpdateSocialNetworks(SocialNetworks socialNetworks)
    {
        SocialNetworks = socialNetworks;
    }

    public void UpdateRequisites(Requisites requisites)
    {
        Requisites = requisites;
    }

    public void Delete()
    {
        _isDeleted = true;

        foreach (var pet in _pets)
            pet.Delete();
    }

    public void Restore()
    {
        _isDeleted = false;

        foreach (var pet in _pets)
            pet.Restore();
    }
}