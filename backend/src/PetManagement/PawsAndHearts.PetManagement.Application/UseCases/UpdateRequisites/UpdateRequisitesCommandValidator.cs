using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.UpdateRequisites;

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