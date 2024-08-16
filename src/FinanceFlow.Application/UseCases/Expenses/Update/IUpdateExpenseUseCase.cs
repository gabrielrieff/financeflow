using FinanceFlow.Communication.Requests;

namespace FinanceFlow.Application.UseCases.Expenses.Update;

public interface IUpdateExpenseUseCase
{
    Task Execute(long id, RequestExpenseJson request);
}
