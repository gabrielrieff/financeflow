
using AutoMapper;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Accounts.GetMonth;

public class GetMonthAccountsUseCase : IGetMonthAccountsUseCase
{

    private readonly IAccountsReadOnlyRepository _repositoryAccount;
    private readonly IReccurenceReadOnlyRepository _repositoryReccurence;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetMonthAccountsUseCase(
        IAccountsReadOnlyRepository repositoryAccount,
        IReccurenceReadOnlyRepository repositoryReccurence,
        IMapper mapper,
        ILoggedUser loggedUser)

    {
        _repositoryAccount = repositoryAccount;
        _repositoryReccurence = repositoryReccurence;

        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<CollectionAccountsResponseJson> Execute(int month, int year)
    {
        var loggedUser = await _loggedUser.Get();

        var accounts = await _repositoryAccount.GetMonth(month: month, year: year, loggedUser.Id);

        var accountsIDs = accounts.Select(a => a.ID).ToList();

        var recurrences = await _repositoryReccurence.GetMonthByID(month, year, accountsIDs);

        var accountsJson = accounts.Select(account => new AccountJson
        {
            ID = account.ID,
            Amount = account.Amount,
            Title = account.Title,
            Description = account.Description,
            TypeAccount = (TypeAccount)account.TypeAccount,
            Tags = (ICollection<Tag>)account.Tags,
            End_Date = recurrences.FirstOrDefault(endDate => endDate.AccountID == account.ID)?.End_Date ?? DateTime.MinValue,
            Start_Date = recurrences.FirstOrDefault(endDate => endDate.AccountID == account.ID)?.Start_Date ?? DateTime.MinValue,
            DateCurrent = account.Create_at,
            InstallmentsCurrent = 1
        }).ToList();


        var response = new CollectionAccountsResponseJson
        {
            responseAccountsJsons = new List<AccountsJson>
            {
                new AccountsJson
                {
                    Month = new DateTime(year, month, 1),
                    Accounts = accountsJson
                }
            }
        };

        return response;
    }
}
