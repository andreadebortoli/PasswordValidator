using Validator.Interfaces;

namespace Validator;

public class ResponseBuilder : IResponseBuilder
{
    private readonly IValidator _lengthValidator;
    private readonly IValidator _twoNumbersValidator;
    private readonly IValidator _specialCharactersValidator;
    private bool _isValid = true;
    private readonly List<string> _messages = new();

    public ResponseBuilder(
      IValidatorFactory validatorFactory)
    {
        _lengthValidator = validatorFactory.GetValidator("length");
        _twoNumbersValidator = validatorFactory.GetValidator("twoNumbers");
        _specialCharactersValidator = validatorFactory.GetValidator("specialCharacters");
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