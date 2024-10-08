using System.Data;

namespace PawsAndHearts.BreedManagement.Application.Interfaces;

public interface IBreedManagementUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}