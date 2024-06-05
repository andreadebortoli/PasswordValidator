using Validator.Interfaces;

namespace Validator;

public class ValidatorFactory : IValidatorFactory
{
    private readonly Dictionary<string, IValidator> _validators;

    public ValidatorFactory(Dictionary<string, IValidator> validators)
    {
        _validators = validators;
    }

    public IValidator GetValidator(string key)
    {
        if (_validators.TryGetValue(key, out var validator))
        {
            return validator;
        }
        throw new KeyNotFoundException($"Validator with key '{key}' not found.");
    }
}