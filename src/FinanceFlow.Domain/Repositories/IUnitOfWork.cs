namespace FinanceFlow.Infrastructure.DataAccess;

public interface IUnitOfWork
{
    Task Commit();
}