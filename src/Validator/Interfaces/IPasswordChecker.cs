namespace Validator.Interfaces;

public interface IPasswordChecker
{
    void CheckPassword(bool isValid, string input);
}