using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdateRequisites;

public class UpdateRequisitesCommandValidator : AbstractValidator<UpdateRequisitesCommand>
{
    public UpdateRequisitesCommandValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
        
        RuleForEach(u => u.Requisites)
            .MustBeValueObject(f =>
                Requisite.Create(f.Name, f.Description));
    }
}