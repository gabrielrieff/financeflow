using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses.Users;

namespace FinanceFlow.Application.UseCases.Users.Register;
public interface IRegisterUserUseCase
{
    public Task<ResponseRegisteredUserJson> Execute(RequestUserJson request);
}
