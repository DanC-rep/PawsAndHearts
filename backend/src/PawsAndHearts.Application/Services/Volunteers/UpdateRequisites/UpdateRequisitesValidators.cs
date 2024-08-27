using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateRequisites;

public class UpdateRequisitesRequestValidator : AbstractValidator<UpdateRequisitesRequest>
{
    public UpdateRequisitesRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateRequisitesDtoValidator : AbstractValidator<UpdateRequisitesDto>
{
    public UpdateRequisitesDtoValidator()
    {
        RuleForEach(u => u.Requisites)
            .MustBeValueObject(f =>
                Requisite.Create(f.Name, f.Description));
    }
}