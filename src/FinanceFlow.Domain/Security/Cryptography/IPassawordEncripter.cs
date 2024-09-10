namespace FinanceFlow.Domain.Security.Cryptography;
public interface IPassawordEncripter
{
    string Encrypt(string password);
}
