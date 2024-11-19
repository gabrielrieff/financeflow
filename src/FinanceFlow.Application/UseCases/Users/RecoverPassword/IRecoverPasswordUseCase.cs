using FinanceFlow.Communication.Requests.Users;

namespace FinanceFlow.Application.UseCases.Users.RecoverPassword;
public interface IRecoverPasswordUseCase
{
    Task Execute(RequestRecoverPasswordJson request);
}
