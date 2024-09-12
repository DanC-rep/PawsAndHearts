using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;

namespace PawsAndHearts.API.Controllers.Volunteers.Requests;

public record UpdateMainInfoRequest(FullNameDto FullName, int Experience, string PhoneNumber)
{
    public UpdateMainInfoCommand ToCommand(Guid volunteerId) =>
        new(
            volunteerId,
            FullName,
            Experience,
            PhoneNumber);
}