using Microsoft.Extensions.DependencyInjection;
using Validator.Interfaces;

namespace Validator;

public class ResponseBuilder
{
    private readonly IValidator _lengthValidator;
    private readonly IValidator _twoNumbersValidator;
    private readonly IValidator _specialCharacterValidator;
    private bool _isValid = true;
    private string _message = string.Empty;

    public ResponseBuilder(
        [FromKeyedServices("lenght")] IValidator lengthValidator,
        [FromKeyedServices("twoNumbers")] IValidator twoNumbersValidator,
        [FromKeyedServices("SpecialCharacters")] IValidator specialCharacterValidator)
    {
        _lengthValidator = lengthValidator;
        _twoNumbersValidator = twoNumbersValidator;
        _specialCharacterValidator = specialCharacterValidator;
    }
    public ResponseBuilder ValidateLenght(string password)
    {
        var result = _lengthValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _message += result.Message;
        }
        return this;
        return this;
    }

    public ResponseBuilder ValidateDigits(string password)
    {
        var result = _twoNumbersValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _message += result.Message;
        }
        return this;
    }

    public ResponseBuilder ValidateSpecialCharacters(string password)
    {
        var result = _specialCharacterValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _message += result.Message;
        }
        return this;
    }

    public bool IsValid => _isValid;
    public string Message => _message;
}