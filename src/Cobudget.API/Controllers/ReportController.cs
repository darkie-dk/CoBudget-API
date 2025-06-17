using CoBudget.Application.UseCases.Reports;
using CoBudget.Communication.Request;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Cobudget.Api.Controllers;

[Route("[controller]")]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel([FromHeader] DateOnly month, [FromServices] IGenerateExpenseReportUseCase useCase)
    {
        byte[] file = await useCase.Execute(month);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, "report.xlsx");
        }

        return NoContent();
    }
}
