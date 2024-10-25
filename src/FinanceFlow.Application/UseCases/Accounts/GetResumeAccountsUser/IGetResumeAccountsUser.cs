using FinanceFlow.Communication.Responses.Account;

namespace FinanceFlow.Application.UseCases.Accounts.GetResumeAccountsUser;
public interface IGetResumeAccountsUser
{
    Task<ResponseGetResumeAccountsUserJson> Execute();
}
