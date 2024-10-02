using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;
using PawsAndHearts.Domain.Volunteer.ValueObjects;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.UpdatePet;

public class UpdatePetValidator : AbstractValidator<UpdatePetCommand>
{
    public UpdatePetValidator()
    {
        RuleFor(v => v.PetId).NotEmpty()
            .WithError(Errors.General.ValueIsRequired("pet id"));
        
        RuleFor(p => p.Name).NotEmpty()
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