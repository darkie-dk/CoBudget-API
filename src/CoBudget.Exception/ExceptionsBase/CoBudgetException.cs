namespace CoBudget.Exception.ExceptionsBase;

public abstract class CoBudgetException : SystemException
{
    protected CoBudgetException(string message) : base(message)
    {
        
    }
}
