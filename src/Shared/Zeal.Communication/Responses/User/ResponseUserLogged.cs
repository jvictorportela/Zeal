namespace Zeal.Communication.Responses.User;

public class ResponseUserLogged
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = default!;
}
