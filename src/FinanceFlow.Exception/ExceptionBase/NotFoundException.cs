using System.Net;

namespace FinanceFlow.Exception.ExceptionBase;

public class NotFoundException: FinanceFlowException
{

    public NotFoundException(string messages) : base(messages)
    {
    }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public override List<string> GetErrors()
    {
        return new List<string>() { Message };
    }
}
