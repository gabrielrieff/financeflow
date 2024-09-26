using AutoMapper;
using FinanceFlow.Application.UseCases.Expenses.Get;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public class GetExpenseUseCase : IGetExpenseUseCase
{
        private readonly IExpensesReadOnlyRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;


    public GetExpenseUseCase(
        IExpensesReadOnlyRepository repositories,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repositories;
        _mapper = mapper;
        _loggedUser = loggedUser;

    }
    public async Task<RequestExpenseJson> Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetById(loggedUser, id);

        if(result is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.EXPENSES_NOT_FOUND);
        }

        return _mapper.Map<RequestExpenseJson>(result);
    }

}
