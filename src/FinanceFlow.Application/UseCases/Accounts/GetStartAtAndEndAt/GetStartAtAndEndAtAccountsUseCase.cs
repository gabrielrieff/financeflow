using AutoMapper;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Accounts.GetStartAtAndEndAt;

public class GetStartAtAndEndAtAccountsUseCase : IGetStartAtAndEndAtAccountsUseCase
{
    private readonly IAccountsReadOnlyRepository _repositoryAccount;
    private readonly IRecurrenceReadOnlyRepository _repositoryReccurence;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetStartAtAndEndAtAccountsUseCase(
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

    public async Task<CollectionAccountsRangeResponseJson> Execute(DateOnly start_at, DateOnly end_at)
    {
        var loggedUser = await _loggedUser.Get();
        var accounts = await _repositoryAccount.GetStartAtAndEndAt(start_at, end_at, loggedUser.Id);
        var accountsIDs = accounts.Select(a => a.ID).ToList();
        var recurrences = await _repositoryReccurence.GetGetStartAtAndEndAtByID(start_at, end_at, accountsIDs);

        var accountsByMonth = new List<AccountRangeJson>();

        foreach (var account in accounts)
        {
            var recurrence = recurrences.FirstOrDefault(r => r.AccountID == account.ID);

            if (recurrence != null)
            {
                // Gera as ocorrências mensais para contas recorrentes
                var currentMonth = new DateTime(recurrence.Start_Date.Year, recurrence.Start_Date.Month, 1);
                var endRecurrenceMonth = new DateTime(recurrence.End_Date.Year, recurrence.End_Date.Month, 1);
                var Installment = 1;

                while (currentMonth <= endRecurrenceMonth)
                {
                    accountsByMonth.Add(new AccountRangeJson
                    {
                        ID = account.ID,
                        Amount = account.Amount,
                        Title = account.Title,
                        Description = account.Description,
                        TypeAccount = (TypeAccount)account.TypeAccount,
                        Tags = account.Tags.Select(tag => (Tag)tag.Value).ToList(),
                        Start_Date = recurrence.Start_Date,
                        End_Date = recurrence.End_Date,
                        DateCurrent = currentMonth,
                        InstallmentsCurrent = Installment
                        
                    });

                    // Avança para o próximo mês
                    currentMonth = currentMonth.AddMonths(1);
                    Installment++;
                }
            }
            else
            {
                // Adiciona contas sem recorrência apenas uma vez
                accountsByMonth.Add(new AccountRangeJson
                {
                    ID = account.ID,
                    Amount = account.Amount,
                    Title = account.Title,
                    Description = account.Description,
                    TypeAccount = (TypeAccount)account.TypeAccount,
                    Tags = account.Tags.Select(tag => (Tag)tag.Value).ToList(),
                    Start_Date = account.Create_at,
                    End_Date = account.Create_at,
                    DateCurrent = account.Create_at,
                    InstallmentsCurrent = 1
                });
            }
        }

        // Cria um dicionário para armazenar as contas por mês
        var groupedByMonth = new Dictionary<DateTime, List<AccountRangeJson>>();
        var currentStartMonth = new DateTime(start_at.Year, start_at.Month, 1);
        var currentEndMonth = new DateTime(end_at.Year, end_at.Month, 1);

        // Inicializa o dicionário com todos os meses do intervalo, mesmo sem contas
        for (var date = currentStartMonth; date <= currentEndMonth; date = date.AddMonths(1))
        {
            groupedByMonth[date] = new List<AccountRangeJson>();
        }

        // Popula os meses com as contas associadas
        foreach (var account in accountsByMonth)
        {
            var accountMonth = new DateTime(account.DateCurrent.Year, account.DateCurrent.Month, 1);
            if (groupedByMonth.ContainsKey(accountMonth))
            {
                groupedByMonth[accountMonth].Add(account);
            }
        }

        // Converte para a estrutura de resposta esperada
        var responseAccountsJsons = groupedByMonth.Select(g => new AccountsRangeJson
        {
            Month = g.Key,
            Accounts = g.Value.ToList()
        }).ToList();

        // Monta a resposta final
        var response = new CollectionAccountsRangeResponseJson
        {
            responseAccountsJsons = responseAccountsJsons
        };

        return response;
    }
}
