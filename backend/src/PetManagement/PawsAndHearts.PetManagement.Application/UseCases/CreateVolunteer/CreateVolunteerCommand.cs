using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreateVolunteer;

public record CreateVolunteerCommand(
    FullNameDto FullName,
    int Experience,
    string PhoneNumber) : ICommand
{
    public static CreateVolunteerCommand Create(CreateVolunteerRequest request) =>
        new(
            request.FullName,
            request.Experience,
            request.PhoneNumber);
}