namespace CoBudget.Exception.ExceptionsBase;

public class ValidationException : CoBudgetException
{
    public List<string> Errors { get; set; }

    public ValidationException(List<string> errors)
    {
        Errors = errors;
    }
}
