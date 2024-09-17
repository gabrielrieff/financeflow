namespace FinanceFlow.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
}