using FinanceFlow.Application.UseCases.Users.Register;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses;
using FinanceFlow.Communication.Responses.Users;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
