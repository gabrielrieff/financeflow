using AutoMapper;
using FinanceFlow.Communication.Responses.Expenses;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;


    public GetAllExpensesUseCase(
        IExpensesReadOnlyRepository repositories,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repository = repositories;
        _mapper = mapper;
        _loggedUser = loggedUser;

    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        var result = await _repository.GetAll(loggedUser);

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    } 
}
