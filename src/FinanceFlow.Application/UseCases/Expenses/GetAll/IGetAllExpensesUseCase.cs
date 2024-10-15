using FinanceFlow.Communication.Responses.Expenses;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
