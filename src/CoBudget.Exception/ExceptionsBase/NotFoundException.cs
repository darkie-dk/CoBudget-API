namespace CoBudget.Exception.ExceptionsBase;

public class NotFoundException : CoBudgetException
{
    public NotFoundException(string message) : base(message)
    {
    }
}
