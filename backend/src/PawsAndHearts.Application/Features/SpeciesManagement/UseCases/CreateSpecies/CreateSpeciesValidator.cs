using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.CreateSpecies;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(s => s.Name).NotEmpty().WithError(Errors.General.ValueIsRequired("name"));
    }
}