
using FinanceFlow.Exception.Resource;
using System.Net;

namespace FinanceFlow.Exception.ExceptionBase;

public class InvalidLoginExpection : FinanceFlowException
{
    public InvalidLoginExpection() : base(ResourceErrorsMessage.EMAIL_OR_PASSWORD_INVALID)
    {

    }

    public override int StatusCode => (int)HttpStatusCode.Unauthorized;

    public override List<string> GetErrors()
    {
        return [Message];
    }
}
