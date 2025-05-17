using CoBudget.Application.UseCases.Expenses.Register;
using CoBudget.Communication.Request;
using Microsoft.AspNetCore.Mvc;

namespace CoBudget.api.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpenseJson request)
    {
        var response = new RegisterExpenseUseCase().Execute(request);

        return Created(string.Empty, response);
    }
}

