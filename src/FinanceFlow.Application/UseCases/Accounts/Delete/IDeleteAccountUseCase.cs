namespace FinanceFlow.Application.UseCases.Accounts.Delete;
public interface IDeleteAccountUseCase
{
    Task Execute(long id);
}
