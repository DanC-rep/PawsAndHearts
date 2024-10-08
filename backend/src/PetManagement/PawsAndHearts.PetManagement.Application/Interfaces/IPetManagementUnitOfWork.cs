using System.Data;

namespace PawsAndHearts.PetManagement.Application.Interfaces;

public interface IPetManagementUnitOfWork
{
    Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default);

    Task SaveChanges(CancellationToken cancellationToken = default);
}