using FinanceFlow.Application.UseCases.Accounts.GetMonth;
using FinanceFlow.Application.UseCases.Accounts.Register;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Communication.Responses.Account;
using FinanceFlow.Communication.Responses.Expenses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]

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

    [HttpGet]
    [ProducesResponseType(typeof(CollectionAccountsResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]

    public async Task<IActionResult> GetMonth(
        [FromQuery] int month,
        [FromQuery] int year,
        [FromServices] IGetMonthAccountsUseCase useCase
        )
    {
        var response = await useCase.Execute(month, year);

        if(response.responseAccountsJsons.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();

    }
}
