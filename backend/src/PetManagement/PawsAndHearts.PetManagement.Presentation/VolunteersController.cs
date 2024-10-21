using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawsAndHearts.Framework;
using PawsAndHearts.Framework.Authorization;
using PawsAndHearts.Framework.Extensions;
using PawsAndHearts.Framework.Processors;
using PawsAndHearts.PetManagement.Application.Queries.GetVolunteerById;
using PawsAndHearts.PetManagement.Application.Queries.GetVolunteersWithPagination;
using PawsAndHearts.PetManagement.Application.UseCases.AddPhotosToPet;
using PawsAndHearts.PetManagement.Application.UseCases.CreatePet;
using PawsAndHearts.PetManagement.Application.UseCases.CreateVolunteer;
using PawsAndHearts.PetManagement.Application.UseCases.DeletePetForce;
using PawsAndHearts.PetManagement.Application.UseCases.DeletePetPhotos;
using PawsAndHearts.PetManagement.Application.UseCases.DeletePetSoft;
using PawsAndHearts.PetManagement.Application.UseCases.DeleteVolunteer;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateMainInfo;
using PawsAndHearts.PetManagement.Application.UseCases.UpdatePet;
using PawsAndHearts.PetManagement.Application.UseCases.UpdatePetMainPhoto;
using PawsAndHearts.PetManagement.Application.UseCases.UpdatePetStatus;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;
using PawsAndHearts.PetManagement.Application.UseCases.UpdateSocialNetworks;
using PawsAndHearts.PetManagement.Contracts.Dtos;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Presentation;

public class VolunteersController : ApplicationController
{
    [Permission("volunteer.create")]
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = CreateVolunteerCommand.Create(request);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("volunteer.update")]
    [HttpPut("{id:guid}/main-info")]
    public async Task<ActionResult<Guid>> UpdateMainInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateMainInfoRequest request,
        [FromServices] UpdateMainInfoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdateMainInfoCommand.Create(id, request);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("volunteer.update")]
    [HttpPut("{id:guid}/social-networks")]
    public async Task<ActionResult<Guid>> UpdateSocialNetworks(
        [FromRoute] Guid id,
        [FromBody] UpdateSocialNetworksRequest request,
        [FromServices] UpdateSocialNetworksHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdateSocialNetworksCommand.Create(id, request);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("volunteer.update")]
    [HttpPut("{id:guid}/requisites")]
    public async Task<ActionResult<Guid>> UpdateRequisites(
        [FromRoute] Guid id,
        [FromBody] UpdateRequisitesRequest request,
        [FromServices] UpdateRequisitesHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdateRequisitesCommand.Create(id, request);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("volunteer.delete")]
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

    [Permission("pet.create")]
    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult<Guid>> CreatePet(
        [FromRoute] Guid id,
        [FromBody] CreatePetRequest request,
        [FromServices] CreatePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = CreatePetCommand.Create(id, request);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.update")]
    [HttpPost("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<ActionResult<FilePathList>> AddPhotosToPet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromForm] AddPhotosToPetRequest request,
        [FromServices] AddPhotosToPetHandler handler,
        CancellationToken cancellationToken = default)
    {
        await using var fileProcessor = new FormFileProcessor();

        var fileDtos = fileProcessor.Process(request.Files);

        var command = AddPhotosToPetCommand.Create(volunteerId, petId, fileDtos);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
    
    [Permission("volunteer.read")]
    [HttpGet]
    public async Task<ActionResult> Get(
        [FromQuery] GetVolunteersWithPaginationRequest request, 
        [FromServices] GetVolunteersWithPaginationHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = GetVolunteersWithPaginationQuery.Create(request);

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }

    [Permission("volunteer.read")]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VolunteerDto>> GetById(
        [FromRoute] Guid id,
        [FromServices] GetVolunteerByIdHandler handler,
        CancellationToken cancellationToken = default)
    {
        var query = new GetVolunteerByIdQuery(id);

        var result = await handler.Handle(query, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.update")]
    [Authorize]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetRequest request,
        [FromServices] UpdatePetHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdatePetCommand.Create(volunteerId, petId, request);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.update")]
    [Authorize]
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<ActionResult<FilePathList>> DeletePetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetPhotosHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetPhotosCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.update")]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/status")]
    public async Task<ActionResult<Guid>> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetStatusRequest request,
        [FromServices] UpdatePetStatusHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdatePetStatusCommand.Create(volunteerId, petId, request);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.delete")]
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/soft")]
    public async Task<ActionResult<Guid>> DeletePetSoft(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetSoftHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetSoftCommand(volunteerId, petId);
        
        var result = await handler.Handle(command, cancellationToken);
        
        return result.ToResponse();
    }

    [Permission("pet.delete")]
    [HttpDelete("{volunteerId}/pet/{petId:guid}/force")]
    public async Task<ActionResult<Guid>> DeletePetForce(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetForceHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = new DeletePetForceCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }

    [Permission("pet.update")]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/mainPhoto")]
    public async Task<ActionResult<FilePath>> UpdatePetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetMainPhotoRequest request,
        [FromServices] UpdatePetMainPhotoHandler handler,
        CancellationToken cancellationToken = default)
    {
        var command = UpdatePetMainPhotoCommand.Create(volunteerId, petId, request);
        
        var result = await handler.Handle(command, cancellationToken);

        return result.ToResponse();
    }
}