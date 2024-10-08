using FinanceFlow.Communication.Responses.Users;

namespace FinanceFlow.Application.UseCases.Users.GetProfile;
public interface IGetProfileUseCase
{
    Task<ResponseUserProfileJson> Execute();
}
