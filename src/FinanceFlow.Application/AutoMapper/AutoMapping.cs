using AutoMapper;
using FinanceFlow.Communication.Requests.Expenses;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses.Expenses;
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
        CreateMap<RequestUserJson, User>()
            .ForMember(dest => dest.Password, config => config.Ignore());

        CreateMap<RequestExpenseJson, Expense>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));

        CreateMap<Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));
    }

    private void EntityToResponse()
    {
        //Expense
        CreateMap<Expense, ResponseExpenseJson>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));

        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, RequestExpenseJson>();
        CreateMap<Expense, ResponseRegisteredExpensesJson>();

        //User
        CreateMap<User, ResponseUserProfileJson>();

    }
}
