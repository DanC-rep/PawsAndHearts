using PawsAndHearts.Domain.Models;
using PawsAndHearts.Domain.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(string Name, string Surname, string? Patronymic, int Experience,
    int PetsFoundHome, int PetsLookingForHome, int PetsBeingTreated, string PhoneNumber, List<Requisite> Requisites,
    List<SocialNetwork> SocialNetworks, List<Pet> Pets);