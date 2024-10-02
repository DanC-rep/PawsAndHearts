using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Controllers.Pets.Requests;
using PawsAndHearts.API.Extensions;
using PawsAndHearts.Application.Dto;
using PawsAndHearts.Application.Features.VolunteerManagement.Queries.GetPetById;
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

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PetDto>> GetById(
        Guid id,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetPetByIdQuery(id);
        
        var result = await handler.Handle(query, cancellationToken);

        return result.ToResponse();
    }
}