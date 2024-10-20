using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using PawsAndHearts.SharedKernel;
using PawsAndHearts.SharedKernel.ValueObjects;

namespace PawsAndHearts.Accounts.Domain;

public class User : IdentityUser<Guid>
{
    public string? Photo { get; set; }
    
    public IReadOnlyList<SocialNetwork> SocialNetworks { get; set; }

    public List<Role> Roles { get; set; } = [];

    public static Result<User, Error> CreateParticipant(
        string userName,
        string email,
        List<SocialNetwork> socialNetworks,
        Role role)
    {
        if (role.Name != Constants.PARTICIPANT)
            return Errors.Accounts.InvalidRole();

        return new User
        {
            UserName = userName,
            Email = email,
            SocialNetworks = socialNetworks,
            Roles = [role]
        };
    }
    
}
