using AutoMapper;
using FinanceFlow.Application.UseCases.Expenses.Get;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Exception;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public class GetExpenseUseCase : IGetExpenseUseCase
{
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;


    public GetExpenseUseCase(IExpensesReadOnlyRepository repositories, IMapper mapper)
    {
        _repository = repositories;
        _mapper = mapper;

    }
    public async Task<RequestExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if(result is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.EXPENSES_NOT_FOUND);
        }

        return _mapper.Map<RequestExpenseJson>(result);
    }

}
