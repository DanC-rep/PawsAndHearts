using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId, 
    FullNameDto FullName, 
    int Experience, 
    string PhoneNumber) : ICommand;