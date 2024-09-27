using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Controllers.Species.Requests;
using PawsAndHearts.API.Extensions;
using PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetBreedsBySpecies;
using PawsAndHearts.Application.Features.SpeciesManagement.Queries.GetSpeciesWithPagination;
using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateBreed;
using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateSpecies;
using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteBreed;
using PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteSpecies;

namespace PawsAndHearts.API.Controllers.Species;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateSpeciesHandler handler,
        [FromBody] CreateSpeciesRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost("{id:guid}/breed")]
    public async Task<ActionResult<Guid>> CreateBreed(
        [FromRoute] Guid id,
        [FromBody] CreateBreedRequest request,
        [FromServices] CreateBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteSpeciesCommand(id);
        
        var result = await handler.Handle(command, cancellationToken);
        
        return result.ToResponse();
    }

    [HttpDelete("{speciesId:guid}/breed/{breedId:guid}")]
    public async Task<ActionResult<Guid>> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteBreedCommand(speciesId, breedId);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [HttpGet("breeds")]
    public async Task<ActionResult> GetBreeds(
        [FromQuery] GetBreedsBySpeciesRequest request,
        [FromServices] GetBreedsBySpeciesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response.Value);
    }
}