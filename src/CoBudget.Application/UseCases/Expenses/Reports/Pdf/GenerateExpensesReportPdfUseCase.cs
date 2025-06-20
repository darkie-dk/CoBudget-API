using CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CoBudget.Domain;
using CoBudget.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using PdfSharp.Fonts;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadRepository _expensesReadRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadRepository repository)
    {
        _expensesReadRepository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _expensesReadRepository.FilterByMonth(date) ?? [];

        var document = CreateDocument(date);
        var page = CreateSection(document);

        var paragraph = page.AddParagraph();
        var title = string.Format(ResourceReportMessages.TOTAL_SPENT_IN, date.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15});
        paragraph.AddLineBreak();

        var totalAmountExpenses =expenses.Sum(expenses => expenses.Amount);
        paragraph.AddFormattedText($"{totalAmountExpenses} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });

        return [];
    }

    private static Document CreateDocument(DateOnly date)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportMessages.EXPENSES_OF} {date:Y}";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        

        return document;
    }

    private static Section CreateSection(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }
}
