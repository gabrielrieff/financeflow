using FinanceFlow.Domain.Entities;

namespace WebApi.Test.Resouces;

public class AccountIdentityManager
{
    private Account _account;

    public AccountIdentityManager(Account account)
    {
        _account = account;
    }

    public long GetId() => _account.ID;

    public DateTime GetDate() => _account.Create_at;

}
