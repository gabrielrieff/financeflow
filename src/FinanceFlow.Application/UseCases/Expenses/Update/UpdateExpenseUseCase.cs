using AutoMapper;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;

namespace FinanceFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;


    public UpdateExpenseUseCase(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IExpensesUpdateOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = repository;
        _loggedUser = loggedUser;
    }

    public async Task Execute(long id, RequestExpenseJson request)
    {

        Validate(request);
        var loggedUser = await _loggedUser.Get();

        var expense = await _repository.GetById(loggedUser, id);

        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.EXPENSES_NOT_FOUND);
        }
        _mapper.Map(request, expense);

        _repository.Update(expense);
        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

}
