using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.SpeciesManagement.UseCases.CreateBreed;

public class CreateBreedValidator : AbstractValidator<CreateBreedCommand>
{
    public CreateBreedValidator()
    {
        RuleFor(b => b.Name).NotEmpty().WithError(Errors.General.ValueIsRequired("name"));
    }
}