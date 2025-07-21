using CoBudget.Application.UseCases.Users.Register;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Cobudget.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}
