using PawsAndHearts.Core.Dtos;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateMainInfo;

namespace PawsAndHearts.PetManagement.Presentation.Volunteers.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, int Experience, string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(
            volunteerId,
            FullName,
            Experience,
            PhoneNumber);
}