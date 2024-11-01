using FinanceFlow.Application.UseCases.Accounts.Delete;
using FinanceFlow.Application.UseCases.Accounts.GetMonth;
using FinanceFlow.Application.UseCases.Accounts.GetResumeAccountsUser;
using FinanceFlow.Application.UseCases.Accounts.GetStartAtAndEndAt;
using FinanceFlow.Application.UseCases.Accounts.Register;
using FinanceFlow.Application.UseCases.Accounts.Update;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Communication.Responses.Account;
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
    [ProducesResponseType(typeof(AccountsJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetMonth(
        [FromQuery] int month,
        [FromQuery] int year,
        [FromServices] IGetMonthAccountsUseCase useCase
        )
    {
        var response = await useCase.Execute(month, year);

        if(response.Accounts.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();

    }
    
    [HttpGet("get-start-at-and-end-at")]
    [ProducesResponseType(typeof(CollectionAccountsResponseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetStartAtAndEndAt(
        [FromQuery] DateOnly start_at,
        [FromQuery] DateOnly end_at,
        [FromServices] IGetStartAtAndEndAtAccountsUseCase useCase
        )
    {
        var response = await useCase.Execute(start_at, end_at);

        if(response.responseAccountsJsons.Count != 0)
        {
            return Ok(response);
        }

        return NoContent();
    }
    
    [HttpGet("Get-resume-accounts-user")]
    [ProducesResponseType(typeof(ResponseGetResumeAccountsUserJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetResumeAccountsUser(
        [FromServices] IGetResumeAccountsUser useCase
        )
    {
        var response = await useCase.Execute();

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteAccountUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);

        return NoContent();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
    [FromServices] IUpdateAccountUseCase useCase,
    [FromRoute] long id,
    [FromBody] AccountRequestJson request)
    {
        await useCase.Execute(id, request);

        return NoContent();
    }

}
