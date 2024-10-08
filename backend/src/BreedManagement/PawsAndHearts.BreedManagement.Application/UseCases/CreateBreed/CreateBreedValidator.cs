using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.UseCases.CreateBreed;

public class CreateBreedValidator : AbstractValidator<CreateBreedCommand>
{
    public CreateBreedValidator()
    {
        RuleFor(b => b.Name).NotEmpty().WithError(Errors.General.ValueIsRequired("name"));
    }
}