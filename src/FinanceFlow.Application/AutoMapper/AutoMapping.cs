using AutoMapper;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses.Account;
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

        //CreateMap<RequestExpenseJson, Expense>()
        //    .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));

        CreateMap<Communication.Enums.Tag, Tag>()
            .ForMember(dest => dest.Value, config => config.MapFrom(source => source));

        //Account
        CreateMap<AccountRequestJson, Account>()
            .ForMember(dest => dest.Tags, config => config.MapFrom(source => source.Tags.Distinct()));
        
        //Recurrence
        CreateMap<AccountRequestJson, Recurrence>();

    }

    private void EntityToResponse()
    {

        //User
        CreateMap<User, ResponseUserProfileJson>();

        //Accounts
        //CreateMap<Account, CollectionAccountsResponseJson>()
        //    .ForMember(dest => dest.responseAccountsJsons
        //                            .Select(x => x.Accounts
        //                            .Select(a => a.Tags)),
        //               config => config.MapFrom(source => source.Tags.Select(tag => tag.Value)));
    }
}
