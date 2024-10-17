﻿using PawsAndHearts.Core.Abstractions;
using PawsAndHearts.PetManagement.Contracts.Requests.Volunteer;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePetMainPhoto;

public record UpdatePetMainPhotoCommand(string FilePath, Guid VolunteerId, Guid PetId) : ICommand
{
    public static UpdatePetMainPhotoCommand Create(
        Guid volunteerId, Guid petId, UpdatePetMainPhotoRequest request) =>
        new(request.FilePath, volunteerId, petId);
}