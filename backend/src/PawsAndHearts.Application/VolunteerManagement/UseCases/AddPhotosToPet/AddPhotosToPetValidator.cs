using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.VolunteerManagement.UseCases.AddPhotosToPet;

public class AddPhotosToPetValidator : AbstractValidator<AddPhotosToPetCommand>
{
    public AddPhotosToPetValidator()
    {
        RuleFor(a => a.VolunteerId).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("volunteer id"));

        RuleFor(a => a.PetId).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("pet id"));

        RuleFor(a => a.Files).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("files"));
    }
}