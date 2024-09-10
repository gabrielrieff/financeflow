using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FinanceFlow.Infrastructure.DataAccess;

namespace FinanceFlow.Application.UseCases.Expenses.DeleteById;

public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IExpensesWhiteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteExpenseUseCase(IExpensesWhiteOnlyRepository repositories, IUnitOfWork unitOfWork)
    {
        _repository = repositories;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var result = await _repository.DeleteById(id);

        if(result == false){
            throw new NotFoundException(ResourceErrorsMessage.EXPENSES_NOT_FOUND);
        }

        await _unitOfWork.Commit();
    }

}
