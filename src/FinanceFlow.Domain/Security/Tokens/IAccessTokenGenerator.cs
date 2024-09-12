using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
