using BC = BCrypt.Net.BCrypt;
using FinanceFlow.Domain.Security.Cryptography;

namespace FinanceFlow.Infrastructure.Security.Cryptography;

public class BCrypt : IPassawordEncripter
{
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        return passwordHash;
    }
}
