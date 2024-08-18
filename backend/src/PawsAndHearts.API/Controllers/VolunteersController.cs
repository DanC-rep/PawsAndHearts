using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

namespace PawsAndHearts.API.Controllers;


[ApiController]
[Route("[controller]")]
public class VolunteersController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request, CancellationToken cancellationToken = default)
    {
        var result = await handler.Handle(request, cancellationToken);

        if (result.IsFailure)
        {
            return BadRequest(result.Error);
        }

        return Ok(result);
    }
}