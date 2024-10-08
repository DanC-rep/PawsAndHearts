using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel;

namespace PawsAndHearts.BreedManagement.Application.UseCases.DeleteBreed;

public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(b => b.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired("breed id"));
    }
}