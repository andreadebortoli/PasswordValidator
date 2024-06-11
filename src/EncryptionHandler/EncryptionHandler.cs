using System.Text;
using System.Security.Cryptography;

namespace EncryptionHandler;

public static class EncryptionHandler
{

    public static string EncryptPassword(string password)
    {
        var encryptedPassword = CalculateSHA256(password);
        var result = Encoding.UTF8.GetString(encryptedPassword);

        return result;
    }


    private static byte[] CalculateSHA256(string passwordToEncrypt)
    {
        SHA256 sha256 = SHA256Managed.Create();
        byte[] hashValue;
        UTF8Encoding objUtf8 = new UTF8Encoding();
        hashValue = sha256.ComputeHash(objUtf8.GetBytes(passwordToEncrypt));

        return hashValue;
    }
}