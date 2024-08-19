using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Extensions;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

namespace PawsAndHearts.API.Controllers;

public class VolunteersController : ApplicationController 
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request, 
        CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }
}