using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateMainInfo;

public class UpdateMainInfoRequestValidator : AbstractValidator<UpdateMainInfoRequest>
{
    public UpdateMainInfoRequestValidator()
    {
        RuleFor(c => c.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateMainInfoDtoValidator : AbstractValidator<UpdateMainInfoDto>
{
    public UpdateMainInfoDtoValidator()
    {
        RuleFor(c => c.PhoneNumber)
            .MustBeValueObject(PhoneNumber.Create);
        
        RuleFor(c => c.FullName)
            .MustBeValueObject(f => 
                FullName.Create(f.Name, f.Surname, f.Patronymic));
        
        RuleFor(c => c.Experience)
            .MustBeValueObject(Experience.Create);
    }
}