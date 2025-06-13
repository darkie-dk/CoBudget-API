namespace CoBudget.Exception.ExceptionsBase;

public abstract class CoBudgetException : SystemException
{
    protected CoBudgetException(string? message) : base(message)
    {
        
    }

    public abstract int StatusCode { get; }
    public abstract List<string> GetErrors();
}
