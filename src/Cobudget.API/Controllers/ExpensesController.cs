using CoBudget.Application.UseCases.Expenses.Delete;
using CoBudget.Application.UseCases.Expenses.GetAll;
using CoBudget.Application.UseCases.Expenses.GetById;
using CoBudget.Application.UseCases.Expenses.Register;
using CoBudget.Communication.Request;
using CoBudget.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CoBudget.api.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpenseJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterExpenseUseCase useCase,
        [FromBody] RequestRegisterExpenseJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseExpensesJson), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllExpenses(
        [FromServices] IGetAllExpensesUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response.Expenses.Count != 0) return Ok(response);

        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseExpenseJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(
        [FromServices] IGetByIdUseCase useCase,
        [FromRoute] long id)
    {
        var response = await useCase.Execute(id);

        if (response.id.Equals(null)) return NotFound(response);

        return Ok(response);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(
        [FromServices] IDeleteExpenseUseCase useCase,
        [FromRoute] long id)
    {
        await useCase.Execute(id);


        return NoContent();
    }
}

