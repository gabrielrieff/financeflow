using FinanceFlow.Communication.Requests.Accounts;

namespace FinanceFlow.Application.UseCases.Accounts.Register;
public interface IRegisterAccountUseCase
{
    Task<string> Execute(AccountRequestJson request);
}
