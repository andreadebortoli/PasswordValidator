using FileHandler;
using Validator.Interfaces;

namespace Validator;

public class PasswordChecker : IPasswordChecker
{
    private readonly IWriter _fileWriter;
    private readonly IPasswordEncryptor _passwordEncryptor;

    public PasswordChecker(IWriter fileWriter, IPasswordEncryptor passwordEncryptor)
    {
        _fileWriter = fileWriter;
        _passwordEncryptor = passwordEncryptor;
    }

    public void CheckPassword(bool isValid, string input)
    {
        if (isValid)
        {
            var encryptedPassword = _passwordEncryptor.EncryptPassword(input);
            _fileWriter.WriteToFile(encryptedPassword);
        }
    }
}