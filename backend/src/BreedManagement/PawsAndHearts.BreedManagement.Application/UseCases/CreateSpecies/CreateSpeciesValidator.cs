using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateSpecies;

public class CreateSpeciesValidator : AbstractValidator<CreateSpeciesCommand>
{
    public CreateSpeciesValidator()
    {
        RuleFor(s => s.Name).NotEmpty().WithError(Errors.General.ValueIsRequired("name"));
    }
}