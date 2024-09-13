using FinanceFlow.Communication.Requests.Login;
using FinanceFlow.Communication.Responses.Users;

namespace FinanceFlow.Application.UseCases.Login;
public interface IDoLoginUseCase
{
    Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request);
}
