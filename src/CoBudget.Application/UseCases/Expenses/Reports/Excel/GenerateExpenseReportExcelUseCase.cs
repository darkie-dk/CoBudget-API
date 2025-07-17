using ClosedXML.Excel;
using CoBudget.Domain;
using CoBudget.Domain.Enum;
using CoBudget.Domain.Extensions;
using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Application.UseCases.Expenses.Reports.Excel;
public class GenerateExpenseReportExcelUseCase(IExpensesReadRepository expensesReadRepository) : IGenerateExpenseReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadRepository _expensesReadRepository = expensesReadRepository;

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _expensesReadRepository.FilterByMonth(date) ?? [];

        using var workbook = new XLWorkbook();

        workbook.Author = "CoBudget";
        workbook.Properties.Title = "Expense Report";

        var reportDate = date.ToString("Y");

        var worksheet = workbook.Worksheets.Add($"{reportDate} expenses");  

        InsertHeader(worksheet);

        var row = 2;
        foreach ( var expense in expenses)
        {
            worksheet.Cell($"A{row}").Value = expense.Title;
            worksheet.Cell($"E{row}").Value = expense.Description;
            worksheet.Cell($"D{row}").Value = expense.Amount;

            worksheet.Cell($"C{row}").Value = expense.PaymentType.PaymentTypeToString();
            worksheet.Cell($"C{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

            worksheet.Cell($"F{row}").Value = ConvertDateTimeOffsetToLocal(expense.Date);

            row++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private static string ConvertDateTimeOffsetToLocal(DateTimeOffset date)
    {
        return date.ToLocalTime().ToString(); 
    }

    private static void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportMessages.CATEGORY;
        worksheet.Cell("C1").Value = ResourceReportMessages.STATUS;
        worksheet.Cell("D1").Value = ResourceReportMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportMessages.PAYMENT_TYPE;
        worksheet.Cell("F1").Value = ResourceReportMessages.DATE;
        worksheet.Cell("G1").Value = ResourceReportMessages.DESCRIPTION;

        var headerRange = worksheet.Range("A1:F1");
        headerRange.Style.Font.Bold = true;
        headerRange.Style.Fill.BackgroundColor = XLColor.AppleGreen;

        worksheet.Cells("A1:F1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        worksheet.Cells("D1").Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

    }
}
