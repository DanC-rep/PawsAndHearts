using CSharpFunctionalExtensions;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Domain.Volunteer.ValueObjects;

public record VolunteerPetsMetrics
{
    private VolunteerPetsMetrics(
        int petsFoundHome, 
        int petsLookingForHome, 
        int petsBeingTreated)
    {
        PetsFoundHome = petsFoundHome;
        PetsLookingForHome = petsLookingForHome;
        PetsBeingTreated = petsBeingTreated;
    }
    
    public int PetsFoundHome { get; }
    
    public int PetsLookingForHome { get; }
    
    public int PetsBeingTreated { get; }

    public static Result<VolunteerPetsMetrics, Error> Create(
        int petsFoundHome, 
        int petsLookingForHome,
        int petsBeingTreated)
    {
        if (petsFoundHome < 0)
            return Errors.General.ValueIsInvalid("pets found home");

        if (petsLookingForHome < 0)
            return Errors.General.ValueIsInvalid("pets looking for home");

        if (petsBeingTreated < 0)
            return Errors.General.ValueIsInvalid("pets being treated");

        return new VolunteerPetsMetrics(petsFoundHome, petsLookingForHome, petsBeingTreated);
    }
}