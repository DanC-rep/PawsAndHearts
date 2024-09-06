using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using PawsAndHearts.Application.Interfaces;

namespace PawsAndHearts.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly PawsAndHeartsDbContext _dbContext;

    public UnitOfWork(PawsAndHeartsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IDbTransaction> BeginTransaction(CancellationToken cancellationToken = default)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        return transaction.GetDbTransaction();
    }

    public async Task SaveChanges(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

}