using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Accounts;

public interface IAccountUpdateOnlyRepository
{
    Task<Account?> GetById(User user, long id);
    void Update(Account account);
}
