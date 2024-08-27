using FluentValidation;
using PawsAndHearts.Application.Validators;
using PawsAndHearts.Domain.Shared;
using PawsAndHearts.Domain.Shared.ValueObjects;

namespace PawsAndHearts.Application.Services.Volunteers.UpdateSocialNetworks;

public class UpdateSocialNetworksRequestValidator : AbstractValidator<UpdateSocialNetworksRequest>
{
    public UpdateSocialNetworksRequestValidator()
    {
        RuleFor(r => r.VolunteerId).NotEmpty().WithError(Errors.General.ValueIsRequired());
    }
}

public class UpdateSocialNetworksDtoValidator : AbstractValidator<UpdateSocialNetworksDto>
{
    public UpdateSocialNetworksDtoValidator()
    {
        RuleForEach(u => u.SocialNetworks)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Link));
    }
}