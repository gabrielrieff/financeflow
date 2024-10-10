using FinanceFlow.Communication.Requests.Users;

namespace FinanceFlow.Application.UseCases.Users.UpdateProfile;
public interface IChangePasswordUseCase
{
    Task Execute(RequestChangePasswordJson request);
}
