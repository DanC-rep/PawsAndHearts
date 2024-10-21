using PawsAndHearts.Accounts.Application.Interfaces;
using PawsAndHearts.Accounts.Domain;

namespace PawsAndHearts.Accounts.Infrastructure.IdentityManagers;

public class AccountsManager(AccountsDbContext accountsDbContext) : IAccountManager
{
    public async Task AddParticipantAccount(
        ParticipantAccount participantAccount, 
        CancellationToken cancellationToken = default)
    {
        await accountsDbContext.ParticipantAccounts.AddAsync(participantAccount, cancellationToken);
    }
}