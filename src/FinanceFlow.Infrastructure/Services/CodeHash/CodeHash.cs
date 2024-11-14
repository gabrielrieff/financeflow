using FinanceFlow.Domain.Services.CodeHash;
using FinanceFlow.Infrastructure.DataAccess;

namespace FinanceFlow.Infrastructure.Services.CodeHash;

public class CodeHash : ICodeHash
{
    private readonly FinanceFlowDbContext _dbContext;

    public CodeHash(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public string CreateCode(int tamanho)
    {
        const string caracteresPermitidos = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        var codigo = "";

        for (int i = 0; i < tamanho; i++)
        {
            codigo += caracteresPermitidos[random.Next(caracteresPermitidos.Length)];
        }

        return codigo;

    }

    public async Task<bool> VerifyCode()
    {
        throw new NotImplementedException();
    }
}
