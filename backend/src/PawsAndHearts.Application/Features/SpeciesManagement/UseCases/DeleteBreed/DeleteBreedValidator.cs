using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;

namespace PawsAndHearts.Application.Features.SpeciesManagement.UseCases.DeleteBreed;

public class DeleteBreedValidator : AbstractValidator<DeleteBreedCommand>
{
    public DeleteBreedValidator()
    {
        RuleFor(b => b.BreedId).NotEmpty().WithError(Errors.General.ValueIsRequired("breed id"));
    }
}