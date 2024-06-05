using Validator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Validator;

public class ResponseBuilder : IResponseBuilder
{
    private readonly IValidator _lengthValidator;
    private readonly IValidator _twoNumbersValidator;
    private readonly IValidator _specialCharactersValidator;
    private bool _isValid = true;
    private readonly List<string> _messages = new();

    public ResponseBuilder(
        [FromKeyedServices("length")] IValidator lengthValidator,
        [FromKeyedServices("twoNumbers")] IValidator twoNumbersValidator,
        [FromKeyedServices("specialCharacters")] IValidator specialCharactersValidator)
    {
        _lengthValidator = lengthValidator;
        _twoNumbersValidator = twoNumbersValidator;
        _specialCharactersValidator = specialCharactersValidator;
    }

    public ResponseBuilder ValidateLength(string password)
    {
        var result = _lengthValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _messages.Add(result.Message);
        }
        return this;
    }

    public ResponseBuilder ValidateDigits(string password)
    {
        var result = _twoNumbersValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _messages.Add(result.Message);
        }
        return this;
    }

    public ResponseBuilder ValidateSpecialCharacters(string password)
    {
        var result = _specialCharactersValidator.Validate(password);
        if (!result.IsValid)
        {
            _isValid = false;
            _messages.Add(result.Message);
        }
        return this;
    }

    public Response Build()
    {
        return new Response
        {
            IsValid = _isValid,
            Message = string.Join("\n", _messages)
        };
    }
}