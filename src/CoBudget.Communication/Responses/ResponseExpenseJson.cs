using CoBudget.Communication.Enum;

namespace CoBudget.Communication.Responses;

public class ResponseExpenseJson
{
    public long id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
    public DateTimeOffset Date { get; set; }
}
