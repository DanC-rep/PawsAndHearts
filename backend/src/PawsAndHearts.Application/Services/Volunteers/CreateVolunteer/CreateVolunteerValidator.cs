using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.CreateVolunteer;

public class CreateVolunteerValidator : AbstractValidator<CreateVolunteerRequest>
{
    public CreateVolunteerValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);

        RuleFor(c => new { c.Name, c.Surname, c.Patronymic })
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