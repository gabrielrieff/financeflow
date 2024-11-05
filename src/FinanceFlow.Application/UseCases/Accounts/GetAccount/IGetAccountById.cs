using FinanceFlow.Communication.Responses.Account;

namespace FinanceFlow.Application.UseCases.Accounts.GetAccount;
public interface IGetAccountById
{
    Task<AccountRangeJson> Execute(long Id);
}
