using FinanceFlow.Communication.Requests.Users;

namespace FinanceFlow.Application.UseCases.Users.UpdateProfile;
public interface IUpdateProfileUseCase
{
    Task Execute(RequestUpdateProfileJson request);
}
