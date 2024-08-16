using AutoMapper;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Application.AutoMapper;

public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
    }

    private void EntityToResponse()
    {
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, RequestExpenseJson>();
        CreateMap<Expense, ResponseRegisteredExpensesJson>();
    }
}
