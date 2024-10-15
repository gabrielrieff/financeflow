using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Responses.Expenses;

namespace FinanceFlow.Application.UseCases.Expenses.Get;

public interface IGetExpenseUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
