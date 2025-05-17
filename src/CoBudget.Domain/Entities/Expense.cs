using CoBudget.Communication.Enum;

namespace CoBudget.Domain.Entities;

internal class Expense
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
}
