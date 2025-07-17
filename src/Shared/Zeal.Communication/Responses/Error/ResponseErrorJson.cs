namespace Zeal.Communication.Responses.Error;

public class ResponseErrorJson
{
    public IList<string> Errors { get; set; }

    public ResponseErrorJson(IList<string> errors) =>  Errors = errors; // constructor to initialize the Errors property

    public bool TokenIsExpired { get; set; }

    public ResponseErrorJson(string errorMessage)
    {
        Errors = new List<string> 
        { 
            errorMessage 
        };
    }
}