namespace Validator.Interfaces;

public interface IValidatorFactory
{
    IValidator GetValidator(string key);
}