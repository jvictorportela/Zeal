using System.Security.Cryptography;
using System.Text;
using Zeal.Domain.Security.Cryptography;

namespace Zeal.Infra.Security.Cryptography;

public class Sha512Encripter : IPasswordEncrypter
{
    private readonly string _aditionalKey;

    public Sha512Encripter(string aditionalKey)
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
