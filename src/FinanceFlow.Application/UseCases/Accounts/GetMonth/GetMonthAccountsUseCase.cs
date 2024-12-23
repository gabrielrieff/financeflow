﻿
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
    private readonly IRecurrenceReadOnlyRepository _repositoryReccurence;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetMonthAccountsUseCase(
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

    public async Task<AccountsJson> Execute(int month, int year)
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
            Tags = account.Tags.Select(tag => (Tag)tag.Value).ToList(),
            End_Date = recurrences.FirstOrDefault(endDate => endDate.AccountID == account.ID)?.End_Date ?? account.Create_at,
            Start_Date = recurrences.FirstOrDefault(endDate => endDate.AccountID == account.ID)?.Start_Date ?? account.Create_at,
            DateCurrent = account.Create_at,
            InstallmentsCurrent = 1
        }).ToList();


        var response =
                new AccountsJson
                {
                    Month = new DateTime(year, month, 1),
                    Accounts = accountsJson
                };

        return response;
    }
}
