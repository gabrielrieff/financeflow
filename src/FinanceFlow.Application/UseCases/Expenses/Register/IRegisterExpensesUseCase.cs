using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Responses;

namespace FinanceFlow.Application.UseCases.Expenses.Register;
public interface IRegisterExpensesUseCase
{
    Task<ResponseRegisteredExpensesJson> Execute(RequestExpenseJson request);
}
