using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePetStatus;

public class UpdatePetStatusValidator : AbstractValidator<UpdatePetStatusCommand>
{
    public UpdatePetStatusValidator()
    {
        RuleFor(p => p.Status).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("help status"));
        
        RuleFor(p => p.Status).Must(s => Constants.PERMITTED_HELP_STATUSES_FROM_VOLUNTEER.Contains(s))
            .WithError(Errors.General.ValueIsInvalid("help status"));
    }
}