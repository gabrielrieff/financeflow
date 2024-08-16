using FinanceFlow.Communication.Responses;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpensesJson> Execute();
}
