using FinanceFlow.Application.UseCases.Accounts.Register;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    [HttpPost]
    //[ProducesResponseType(typeof(ResponseRegisteredExpensesJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromBody] AccountRequestJson request,
        [FromServices] IRegisterAccountUseCase useCase
        )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
