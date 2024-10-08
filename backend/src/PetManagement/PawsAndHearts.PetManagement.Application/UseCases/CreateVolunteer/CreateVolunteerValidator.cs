using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.PetManagement.Application.UseCases.CreateVolunteer;

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