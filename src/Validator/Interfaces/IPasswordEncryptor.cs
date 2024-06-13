namespace Validator.Interfaces;

public interface IPasswordEncryptor
{
    string EncryptPassword(string input);
}