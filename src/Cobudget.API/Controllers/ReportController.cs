using CoBudget.Application.UseCases.Expenses.Reports.Excel;
using CoBudget.Application.UseCases.Expenses.Reports.Pdf;
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
    public async Task<IActionResult> GetExcel([FromQuery] DateOnly date, [FromServices] IGenerateExpenseReportExcelUseCase useCase)
    {
        byte[] file = await useCase.Execute(date);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Octet, $"report_{date}.xlsx");
        }

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPdf([FromQuery] DateOnly date, [FromServices] IGenerateExpensesReportPdfUseCase useCase)
    {
        byte[] file = await useCase.Execute(date);

        if (file.Length > 0)
        {
            return File(file, MediaTypeNames.Application.Pdf, $"report_{date}.pdf");
        }

        return NoContent();
        }
}
