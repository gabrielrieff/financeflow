using FinanceFlow.Communication.Requests.Expenses;

namespace FinanceFlow.Application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}
