using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Users;
public interface IUserWhiteOnlyRepository
{
    Task Add(User user);
}
