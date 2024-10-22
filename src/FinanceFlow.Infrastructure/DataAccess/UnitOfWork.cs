
using FinanceFlow.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FinanceFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{

    private IDbContextTransaction _transaction;
    public FinanceFlowDbContext _dbContext;

    public UnitOfWork(FinanceFlowDbContext dbContext) => _dbContext = dbContext;

    public async Task Commit() => await _dbContext.SaveChangesAsync();
    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            await _transaction?.CommitAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync()
    {
        try
        {
            await _transaction?.RollbackAsync();
        }
        finally
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }
    }
}
