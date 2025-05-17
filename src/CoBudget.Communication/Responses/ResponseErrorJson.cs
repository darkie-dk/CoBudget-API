namespace CoBudget.Communication.Responses;

public class ResponseErrorJson
{
    public List<String> ErrorMessage { get; set; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorMessage = [errorMessage];
    }

    public ResponseErrorJson(List<string> errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
