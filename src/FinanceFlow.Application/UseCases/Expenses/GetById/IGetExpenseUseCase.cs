using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Responses;

namespace FinanceFlow.Application.UseCases.Expenses.Get;

public interface IGetExpenseUseCase
{
    Task<ResponseExpenseJson> Execute(long id);
}
