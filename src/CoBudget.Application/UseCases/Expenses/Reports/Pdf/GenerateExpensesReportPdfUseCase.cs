using CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CoBudget.Domain.Repositories.Expenses;
using PdfSharp.Fonts;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    private readonly IExpensesReadRepository _expensesReadRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadRepository repository)
    {
        _expensesReadRepository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _expensesReadRepository.FilterByMonth(date) ?? [];

        return [];
    }
}
