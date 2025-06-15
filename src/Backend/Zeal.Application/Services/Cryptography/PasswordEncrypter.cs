using System.Security.Cryptography;
using System.Text;

namespace Zeal.Application.Services.Cryptography;

public class PasswordEncrypter
{
    private readonly string _aditionalKey;

    public PasswordEncrypter(string aditionalKey)
    {
        _aditionalKey = aditionalKey;
    }

    public string Encrypt(string password)
    {
        var newPassword = $"{password}{_aditionalKey}";

        var bytes = Encoding.UTF8.GetBytes(newPassword);
        var hashBytes = SHA512.HashData(bytes);

        return StringBytes(hashBytes);
    }

    private static string StringBytes(byte[] bytes)
    {
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            var hex = b.ToString("x2");
            sb.Append(hex);
        }

        return sb.ToString();
    }
}