using PawsAndHearts.Accounts.Domain;

namespace PawsAndHearts.Accounts.Application.Interfaces;

public interface IAccountManager
{
    Task AddParticipantAccount(ParticipantAccount account, CancellationToken cancellationToken = default);
}