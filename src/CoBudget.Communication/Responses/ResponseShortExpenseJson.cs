namespace CoBudget.Communication.Responses;

public class ResponseShortExpenseJson
{
    public long id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}
