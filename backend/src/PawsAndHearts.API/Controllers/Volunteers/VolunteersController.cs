using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Controllers.Volunteers.Requests;
using PawsAndHearts.API.Extensions;
using PawsAndHearts.API.Processors;
using PawsAndHearts.Application.Services.Volunteers.AddPhotosToPet;
using PawsAndHearts.Application.Services.Volunteers.CreatePet;
using PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;
using PawsAndHearts.Application.Services.Volunteers.DeleteVolunteer;
using PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;
using PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;
using PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.API.Controllers.Volunteers;

public class VolunteersController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand();
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateMainInfoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult<Guid>> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialNetworksRequest request,
        [FromServices] UpdateSocialNetworksHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] UpdateRequisitesRequest request,
        [FromServices] UpdateRequisitesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteVolunteerCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult<Guid>> CreatePet(
        [FromRoute] Guid id,
        [FromBody] CreatePetRequest request,
        [FromServices] CreatePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = request.ToCommand(id);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost("{id:guid}/pet/photos")]
    public async Task<ActionResult<FilePathList>> AddPhotosToPet(
        [FromRoute] Guid id,
        [FromForm] AddPhotosToPetRequest request,
        [FromServices] AddPhotosToPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.Files);

        var command = request.ToCommand(id, fileDtos);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}