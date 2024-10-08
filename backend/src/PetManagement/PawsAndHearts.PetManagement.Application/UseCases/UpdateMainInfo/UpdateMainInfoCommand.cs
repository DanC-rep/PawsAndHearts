using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId, 
    FullNameDto FullName, 
    int Experience, 
    string PhoneNumber) : ICommand;