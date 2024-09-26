using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;

namespace FinanceFlow.Application.UseCases.Expenses.DeleteById;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _expensesReadOnly;
    private readonly IExpensesWhiteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;

    public DeleteExpenseUseCase(
        IExpensesReadOnlyRepository expensesReadOnly,
        IExpensesWhiteOnlyRepository repositories,
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _expensesReadOnly = expensesReadOnly;
        _repository = repositories;
        _unitOfWork = unitOfWork;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var expense = await _expensesReadOnly.GetById(loggedUser, id);

        if(expense is null){
            throw new NotFoundException(ResourceErrorsMessage.EXPENSES_NOT_FOUND);
        }

        await _repository.DeleteById(id);


        await _unitOfWork.Commit();
    }

}
