using AutoMapper;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using Tag = FinanceFlow.Communication.Enums.Tag;

namespace FinanceFlow.Application.UseCases.Accounts.GetAccount;

public class GetAccountById : IGetAccountById
{
    private readonly IAccountsReadOnlyRepository _repositoryAccount;
    private readonly IRecurrenceReadOnlyRepository _repositoryReccurence;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetAccountById(
        IAccountsReadOnlyRepository repositoryAccount,
        IRecurrenceReadOnlyRepository repositoryReccurence,
        IMapper mapper,
        ILoggedUser loggedUser)

    {
        _repositoryAccount = repositoryAccount;
        _repositoryReccurence = repositoryReccurence;

        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<AccountRangeJson> Execute(long Id)
    {
        var loggedUser = await _loggedUser.Get();
        var account = await _repositoryAccount.GetById(loggedUser, Id);

        if(account is null)
        {
            throw new NotFoundException("Account not found.");
        }

        var accountsID = account.ID;
        var recurrence = await _repositoryReccurence.GetRecurrenceById(account.ID);

        var result = new AccountRangeJson
        {
            ID = account.ID,
            Amount = account.Amount,
            Title = account.Title,
            Description = account.Description,
            TypeAccount = (TypeAccount)account.TypeAccount,
            Tags = account.Tags.Select(tag => (Tag)tag.Value).ToList(),
            End_Date = recurrence?.End_Date ?? account.Create_at,
            Start_Date = recurrence?.Start_Date ?? account.Create_at,
            DateCurrent = account.Create_at,
            InstallmentsCurrent = 1
        };

        return result;

    }
}
