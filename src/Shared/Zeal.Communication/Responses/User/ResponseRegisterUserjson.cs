namespace Zeal.Communication.Responses.User;

public class ResponseRegisterUserjson
{
    public string Name { get; set; } = string.Empty;
    public ResponseTokensJson Tokens { get; set; } = default!;
}
