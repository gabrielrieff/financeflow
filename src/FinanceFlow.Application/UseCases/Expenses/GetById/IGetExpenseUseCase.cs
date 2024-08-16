using FinanceFlow.Communication.Requests;

namespace FinanceFlow.Application.UseCases.Expenses.Get;

public interface IGetExpenseUseCase
{
    Task<RequestExpenseJson> Execute(long id);
}
