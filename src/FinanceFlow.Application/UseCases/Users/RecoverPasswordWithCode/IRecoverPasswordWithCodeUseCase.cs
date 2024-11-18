using FinanceFlow.Communication.Requests.Users;

namespace FinanceFlow.Application.UseCases.Users.RecoverPasswordWithCode;
public interface IRecoverPasswordWithCodeUseCase
{
    Task Execute(RequestRecoverPasswordWithCode request);
}
