using System.Net;

namespace CoBudget.Exception.ExceptionsBase;
public class InvalidLoginException : CoBudgetException
{
    public override int StatusCode => (int)HttpStatusCode.Unauthorized;
    public InvalidLoginException() : base(ResourceErrorMessages.EMAIL_OR_PASSWORD_INVALID)
    {
    }
    public override List<string> GetErrors()
    {
        return [Message];
    }
}
{
}
