using FinanceFlow.Communication.Requests.Expenses;
using FinanceFlow.Communication.Responses.Expenses;

namespace FinanceFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpensesUseCase
{
    Task<ResponseRegisteredExpensesJson> Execute(RequestExpenseJson request);
}
