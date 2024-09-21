using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName, 
    int Experience,
    string PhoneNumber, 
    IEnumerable<RequisiteDto> Requisites, 
    IEnumerable<SocialNetworkDto> SocialNetworks) : ICommand;