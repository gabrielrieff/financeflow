using AutoMapper;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Domain.Repositories.Expenses;

namespace FinanceFlow.Application.UseCases.Expenses.GetAll;

public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpensesReadOnlyRepository repositories, IMapper mapper)
    {
        _repository = repositories;
        _mapper = mapper;

    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    } 
}
