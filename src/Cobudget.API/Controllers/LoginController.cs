using CoBudget.Application.UseCases.Login;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Cobudget.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequestLoginJSON request
        )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
