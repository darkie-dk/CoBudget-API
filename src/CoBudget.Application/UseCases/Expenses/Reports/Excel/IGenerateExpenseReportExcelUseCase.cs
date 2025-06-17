namespace CoBudget.Application.UseCases.Expenses.Reports.Excel;
public interface IGenerateExpenseReportExcelUseCase
{
    Task<byte[]> Execute(DateOnly date);
}
