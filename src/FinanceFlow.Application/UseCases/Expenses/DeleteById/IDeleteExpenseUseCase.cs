using FinanceFlow.Communication.Responses;

namespace FinanceFlow.Application.UseCases.Expenses.DeleteById;

public interface IDeleteExpenseUseCase
{
    Task Execute(long id);
}
