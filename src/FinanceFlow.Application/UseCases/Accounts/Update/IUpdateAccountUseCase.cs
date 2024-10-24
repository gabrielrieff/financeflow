using FinanceFlow.Communication.Requests.Accounts;

namespace FinanceFlow.Application.UseCases.Accounts.Update;
public interface IUpdateAccountUseCase
{
    Task Execute(long id, AccountRequestJson request);
}
