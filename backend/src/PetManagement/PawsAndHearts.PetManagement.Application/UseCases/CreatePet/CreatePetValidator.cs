using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.PetManagement.Domain.ValueObjects;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreatePet;

public class CreatePetValidator : AbstractValidator<CreatePetCommand>
{
    public CreatePetValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("name"));

        RuleFor(c => c.Description).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("description"));

        RuleFor(c => c.Color)
            .MustBeValueObject(Color.Create);

        RuleFor(c => c.HealthInfo).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("health info"));

        RuleFor(c => c.Address)
            .MustBeValueObject(f =>
                Address.Create(f.City, f.Street, f.House, f.Flat));

        RuleFor(c => c.PetMetrics)
            .MustBeValueObject(f =>
                PetMetrics.Create(f.Height, f.Weight));

        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.BirthDate)
            .MustBeValueObject(BirthDate.Create);

        RuleFor(c => c.CreationDate)
            .MustBeValueObject(CreationDate.Create);

        RuleForEach(c => c.Requisites)
            .MustBeValueObject(f => Requisite.Create(f.Name, f.Description));
    }
}