namespace CoBudget.Exception.ExceptionsBase;

public abstract class CoBudgetException(string? message) : SystemException(message)
{
    public abstract int StatusCode { get; }

    public abstract List<string> GetErrors();
}
