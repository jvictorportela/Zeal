namespace Zeal.Exceptions.ExceptionsBase;

public class InvalidLoginException : ZealException
{
    public InvalidLoginException() : base(ResourceMessagesExceptions.EMAIL_OR_PASSWORD_INVALID)
    {
        
    }
}
