using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.Core.Dtos;
using PawsAndHearts.Framework;
using PawsAndHearts.Framework.Extensions;
using PawsAndHearts.PetManagement.Application.Queries.GetPetById;
using PawsAndHearts.PetManagement.Application.Queries.GetPetsWIthPagination;
using PawsAndHearts.PetManagement.Presentation.Pets.Requests;

namespace PawsAndHearts.PetManagement.Presentation.Pets;

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