using AutoMapper;
using FinanceFlow.Communication.Requests;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Communication.Responses.Users;
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
        CreateMap<RequestUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        //Expense
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, RequestExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
        CreateMap<Expense, ResponseRegisteredExpensesJson>();

        //User
        CreateMap<User, ResponseUserProfileJson>();

    }
}
