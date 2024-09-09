using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.API.Contracts;
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

    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoDto dto,
        [FromServices] UpdateMainInfoHandler handler,
        [FromServices] IValidator<UpdateMainInfoRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateMainInfoRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();

        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult<Guid>> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialNetworksDto dto,
        [FromServices] UpdateSocialNetworksHandler handler,
        [FromServices] IValidator<UpdateSocialNetworksRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateSocialNetworksRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();

        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] UpdateRequisitesDto dto,
        [FromServices] UpdateRequisitesHandler handler,
        [FromServices] IValidator<UpdateRequisitesRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateRequisitesRequest(id, dto);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();

        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> Delete(
        [FromRoute] Guid id,
        [FromServices] DeleteVolunteerHandler handler,
        [FromServices] IValidator<DeleteVolunteerRequest> validator,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteVolunteerRequest(id);

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();

        var result = await handler.Handle(request, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult<Guid>> CreatePet(
        [FromRoute] Guid id,
        [FromBody] CreatePetRequest request,
        [FromServices] CreatePetHandler handler,
        [FromServices] IValidator<CreatePetCommand> validator,
        CancellationToken cancellationToken = default)
    {
        var command = new CreatePetCommand(
            id,
            request.Name,
            request.Description,
            request.Color,
            request.HealthInfo,
            request.Address,
            request.PetMetrics,
            request.PhoneNumber,
            request.IsNeutered,
            request.BirthDate,
            request.IsVaccinated,
            request.HelpStatus,
            request.CreationDate,
            request.Requisites);
        
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [HttpPost("{id:guid}/pet/photos")]
    public async Task<ActionResult<FilePathList>> AddPhotosToPet(
        [FromRoute] Guid id,
        [FromForm] AddPhotosToPetRequest request,
        [FromServices] AddPhotosToPetHandler handler,
        [FromServices] IValidator<AddPhotosToPetCommand> validator,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.Files);

        var command = new AddPhotosToPetCommand(id, request.PetId, fileDtos);

        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            return validationResult.ToValidationErrorsResponse();
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}