namespace FinanceFlow.Exception.ExceptionBase;

public abstract class FinanceFlowException : SystemException
{
    protected FinanceFlowException(string message) : base(message)
    {}

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
