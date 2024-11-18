namespace FinanceFlow.Domain.Services.CodeHash;
public interface ICodeHash
{
    string CreateCode(int tamanho = 6);

    Task<bool> VerifyCode(string code, string email);
}
