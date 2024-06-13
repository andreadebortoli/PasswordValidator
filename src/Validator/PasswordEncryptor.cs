using System.Text;
using System.Security.Cryptography;
using Validator.Interfaces;

namespace Validator;

public class PasswordEncryptor : IPasswordEncryptor
{
    public string EncryptPassword(string input)
    {
        var hashedValue = CalculateSHA256(input);
        var result = Convert.ToBase64String(hashedValue); ;

        return result;
    }

    private byte[] CalculateSHA256(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        return sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
    }
}