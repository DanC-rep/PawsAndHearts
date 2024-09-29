using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetPhotos;

public class UpdatePetPhotosValidator : AbstractValidator<UpdatePetPhotosCommand>
{
    public UpdatePetPhotosValidator()
    {
        RuleFor(u => u.FileDtos).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("files"));
    }
}