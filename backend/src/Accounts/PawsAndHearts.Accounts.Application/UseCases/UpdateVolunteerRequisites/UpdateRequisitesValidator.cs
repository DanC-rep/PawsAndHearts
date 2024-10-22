using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateVolunteerRequisites;

public class UpdateRequisitesValidator : AbstractValidator<UpdateVolunteerRequisitesCommand>
{
    public UpdateRequisitesValidator()
    {
        RuleForEach(c => c.Requisites)
            .MustBeValueObject(f => 
                Requisite.Create(f.Name, f.Description));
    }
}