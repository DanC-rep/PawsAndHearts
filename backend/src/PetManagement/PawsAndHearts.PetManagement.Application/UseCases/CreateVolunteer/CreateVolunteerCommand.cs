using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName, 
    int Experience,
    string PhoneNumber, 
    IEnumerable<RequisiteDto> Requisites, 
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;