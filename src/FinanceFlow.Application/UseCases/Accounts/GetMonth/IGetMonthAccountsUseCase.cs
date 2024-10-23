using FinanceFlow.Communication.Responses.Account;

namespace FinanceFlow.Application.UseCases.Accounts.GetMonth;
public interface IGetMonthAccountsUseCase
{
    Task<List<ResponseAccountsJson>> Execute(int month, int year);
}
