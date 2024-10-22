using FluentValidation;
using PawsAndHearts.Core.Validation;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Application.UseCases.UpdateUserSocialNetworks;

public class UpdateSocialNetworksValidator : AbstractValidator<UpdateUserSocialNetworksCommand>
{
    public UpdateSocialNetworksValidator()
    {
        RuleForEach(c => c.SocialNetworks)
            .MustBeValueObject(f => 
                SocialNetwork.Create(f.Name, f.Link));
    }
}