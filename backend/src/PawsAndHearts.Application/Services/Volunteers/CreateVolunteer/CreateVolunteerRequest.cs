using PawsAndHearts.Application.Dto;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public record CreateVolunteerRequest(
    string Name, 
    string Surname, 
    string? Patronymic, 
    int Experience,
    int PetsFoundHome, 
    int PetsLookingForHome, 
    int PetsBeingTreated, 
    string PhoneNumber, 
    IEnumerable<RequisiteDto>? Requisites, 
    IEnumerable<SocialNetworkDto>? SocialNetworks);