using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Features.VolunteerManagement.UseCases.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerCommand>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => c.Experience)
            .MustBeValueObject(Experience.Create);

        RuleFor(c => c.FullName)
            .MustBeValueObject(f => 
                FullName.Create(f.Name, f.Surname, f.Patronymic));

        RuleForEach(c => c.SocialNetworks)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Link));
        
        RuleForEach(c => c.Requisites)
            .MustBeValueObject(f => 
                Requisite.Create(f.Name, f.Description));
    }
}