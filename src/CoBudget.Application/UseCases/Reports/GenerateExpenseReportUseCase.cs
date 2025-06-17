
using System.IO;
using ClosedXML.Excel;
using CoBudget.Domain;

namespace CoBudget.Application.UseCases.Reports;
public class GenerateExpenseReportUseCase : IGenerateExpenseReportUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        var workbook = new XLWorkbook();

        workbook.Author = "CoBudget";
        workbook.Properties.Title = "Expense Report";

        var reportDate = month.ToString("Y");

        var worksheet = workbook.Worksheets.Add($"{reportDate} expenses");

        InsertHeader(worksheet);

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportTableHeaders.TITLE;
        worksheet.Cell("B1").Value = ResourceReportTableHeaders.CATEGORY;
        worksheet.Cell("C1").Value = ResourceReportTableHeaders.STATUS;
        worksheet.Cell("D1").Value = ResourceReportTableHeaders.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportTableHeaders.PAYMENT_TYPE;
        worksheet.Cell("F1").Value = ResourceReportTableHeaders.DATE;

        var headerRange = worksheet.Range("A1:F1");
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.AppleGreen;

        worksheet.Cells("A1:F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cells("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

    }
}
