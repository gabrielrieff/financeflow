using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Accounts;

public interface IAccountsReadOnlyRepository
{
    Task<List<Account>> GetMonth(int month, int year, long userId);

    Task<Account?> GetById(User user, long id);

}
