
using AutoMapper;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Repositories.Transactions;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Accounts.GetMonth;

public class GetMonthAccountsUseCase : IGetMonthAccountsUseCase
{

    private readonly IAccountsReadOnlyRepository _repositoryAccount;
    private readonly IReccurenceReadOnlyRepository _repositoryReccurence;
    private readonly ITransactionReadOnlyRepository _repositoryTransaction;

    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetMonthAccountsUseCase(
        IAccountsReadOnlyRepository repositoryAccount,
        IReccurenceReadOnlyRepository repositoryReccurence,
        ITransactionReadOnlyRepository repositoryTransaction,
        IMapper mapper,
        ILoggedUser loggedUser)

    {
        _repositoryAccount = repositoryAccount;
        _repositoryReccurence = repositoryReccurence;
        _repositoryTransaction = repositoryTransaction;

        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<List<ResponseAccountsJson>> Execute(int month, int year)
    {
        var loggedUser = await _loggedUser.Get();

        var accounts = await _repositoryAccount.GetMonth(month: month, year: year, loggedUser.Id);

        var accountsIDs = accounts.Select(a => a.ID).ToList();

        var recurrences = await _repositoryReccurence.GetMonthByID(month, year, accountsIDs);

        var transactions = await _repositoryTransaction.GetMonthByID(accountsIDs);

        var accountsMapper = _mapper.Map<List<ResponseAccountsJson>>(accounts);

        foreach(var account in accountsMapper)
        {
            var recurrencesFiltered = recurrences.Where(r => r.AccountID == account.ID).FirstOrDefault();
            if (recurrencesFiltered is not null)
            {
                account.RecurrenceResponseJson = _mapper.Map<RecurrenceResponseJson>(recurrencesFiltered);
            }

            var transactionsFiltered = transactions.Where(t => t.AccountID == account.ID).ToList();
            if (transactionsFiltered.Any())
            {
                account.TransactionResponseJson = _mapper.Map<List<TransactionResponseJson>>(transactionsFiltered);
            }

        }

        return accountsMapper;
    }
}
