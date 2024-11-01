using FinanceFlow.Communication.Responses.Account;

namespace FinanceFlow.Application.UseCases.Accounts.GetMonth;
public interface IGetMonthAccountsUseCase
{
    Task<AccountsJson> Execute(int month, int year);
}
