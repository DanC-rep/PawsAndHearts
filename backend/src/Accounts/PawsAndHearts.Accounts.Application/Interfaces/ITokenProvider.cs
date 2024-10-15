using PawsAndHearts.Accounts.Domain;

namespace PawsAndHearts.Accounts.Application.Interfaces;

public interface ITokenProvider
{
   string GenerateAccessToken(User user, CancellationToken cancellationToken = default);
}