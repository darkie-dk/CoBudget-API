using CoBudget.Communication.Enum;

namespace CoBudget.Communication.Request;

public class RequestRegisterExpenseJson
{
    public string Title { get; set; } = string.Empty;   
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public ExpenseType ExpenseType { get; set; }
}

