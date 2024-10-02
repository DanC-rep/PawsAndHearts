using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Controllers.Pets.Requests;
using PawsAndHearts.API.Controllers.Volunteers.Requests;
using PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetPetsWIthPagination;

namespace PawsAndHearts.API.Controllers.Pets;

public class PetsController : ApplicationController
{
    [HttpGet]
    public async Task<ActionResult> GetPets(
        [FromQuery] GetPetsWithPaginationRequest request,
        [FromServices] GetPetsWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
}