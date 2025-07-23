namespace CoBudget.Exception.ExceptionsBase;

public abstract class CoBudgetException : SystemException
{
    public abstract int StatusCode { get; }
    protected CoBudgetException(string? message) : base(message)
    {
        
    }

    public abstract List<string> GetErrors();
}
