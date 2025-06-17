using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase(IExpensesReadRepository repository) : IGenerateExpensesReportPdfUseCase
{
    private IExpensesReadRepository _expensesReadRepository = repository;
    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _expensesReadRepository.FilterByMonth(date) ?? [];

        return [];
    }
}
