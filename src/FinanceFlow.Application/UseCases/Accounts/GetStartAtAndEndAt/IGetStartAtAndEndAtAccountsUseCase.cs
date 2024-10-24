using FinanceFlow.Communication.Responses.Account;

namespace FinanceFlow.Application.UseCases.Accounts.GetStartAtAndEndAt;
public interface IGetStartAtAndEndAtAccountsUseCase
{
    Task<CollectionAccountsRangeResponseJson> Execute(DateOnly start_at, DateOnly end_at);
}
