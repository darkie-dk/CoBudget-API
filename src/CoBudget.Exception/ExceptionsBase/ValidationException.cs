using System.Net;

namespace CoBudget.Exception.ExceptionsBase;

public class ValidationException : CoBudgetException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ValidationException(List<string> errors) : base(string.Empty)
    {
        _errors = errors;
    }
    public override List<string> GetErrors()
    {
        return _errors;
    }
}
