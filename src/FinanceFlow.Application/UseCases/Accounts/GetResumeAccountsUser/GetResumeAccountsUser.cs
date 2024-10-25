using AutoMapper;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Accounts.GetResumeAccountsUser;

public class GetResumeAccountsUser : IGetResumeAccountsUser
{
    private readonly IAccountsReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public GetResumeAccountsUser(
        IAccountsReadOnlyRepository repositoryAccount,
        IMapper mapper,
        ILoggedUser loggedUser)

    {
        _repository = repositoryAccount;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<ResponseGetResumeAccountsUserJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();
        var date = DateTime.Now;
        var accounts = await _repository.GetMonth(month: date.Month, year: date.Year, loggedUser.Id);

        if(accounts is null)
        {
            return new ResponseGetResumeAccountsUserJson
            {
                Expenses = 0,
                Revenues = 0,
            };
        }

        return new ResponseGetResumeAccountsUserJson
        {
            Expenses = accounts.Where(e => (int)e.TypeAccount == 0).Sum(c => c.Amount / c.Installment),
            Revenues = accounts.Where(e => (int)e.TypeAccount == 1).Sum(c => c.Amount),
        };
    }
}
