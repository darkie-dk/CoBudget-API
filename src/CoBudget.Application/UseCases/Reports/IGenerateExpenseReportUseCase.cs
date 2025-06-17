namespace CoBudget.Application.UseCases.Reports;
public interface IGenerateExpenseReportUseCase
{
    Task<byte[]> Execute(DateOnly month);
}
