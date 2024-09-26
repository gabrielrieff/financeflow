using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Services.LoggedUser;
public interface ILoggedUser
{
    Task<User> Get();
}
