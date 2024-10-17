using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateMainInfo;

public record UpdateMainInfoCommand(
    Guid VolunteerId,
    FullNameDto FullName,
    int Experience,
    string PhoneNumber) : ICommand
{
    public static UpdateMainInfoCommand Create(Guid volunteerId, UpdateMainInfoRequest request) =>
        new(
            volunteerId,
            request.FullName,
            request.Experience,
            request.PhoneNumber);
}