using FinanceFlow.Communication.Responses;
using FinanceFlow.Exception;
using FinanceFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinanceFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is FinanceFlowException)
        {
            HandleProjectException(context);
        }
        else
        {
            ThworUnkowError(context);
        }
    }

    private void HandleProjectException(ExceptionContext context)
    {
        var financeFlowException = (FinanceFlowException)context.Exception;
        var errorMessage = new ResponseErrorJson(financeFlowException.GetErrors());

        context.HttpContext.Response.StatusCode = financeFlowException.StatusCode;
        context.Result = new ObjectResult(errorMessage);

    }

    private void ThworUnkowError(ExceptionContext context)
    {
        var errorMessage = new ResponseErrorJson(ResourceErrorsMessage.UNKNOWN_ERROR);

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        context.Result = new ObjectResult(errorMessage);
    }
}
