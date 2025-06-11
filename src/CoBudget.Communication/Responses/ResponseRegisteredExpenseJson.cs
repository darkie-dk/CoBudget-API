namespace CoBudget.Communication.Responses;

public class ResponseRegisteredExpenseJson
{
    public int ExpenseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

}

