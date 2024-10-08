using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdatePetStatus;

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