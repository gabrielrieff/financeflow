using AutoMapper;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Infrastructure.DataAccess;

namespace FinanceFlow.Application.UseCases.Expenses.Register;

public class RegisterExpensesUseCase : IRegisterExpensesUseCase
{
    private readonly IExpensesWhiteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RegisterExpensesUseCase(
        IExpensesWhiteOnlyRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper
        )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseRegisteredExpensesJson> Execute(RequestExpenseJson request)
    {
        Validate(request);

        var entity = _mapper.Map<Expense>(request);

        await _repository.Add(entity);

        await _unitOfWork.Commit();

        var t = _mapper.Map<ResponseRegisteredExpensesJson>(entity);

        return t;
    }

    private void Validate(RequestExpenseJson entidade)
    {

        var validator = new ExpenseValidator();

        var result = validator.Validate(entidade);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }
}
