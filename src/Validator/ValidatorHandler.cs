using Validator.Interfaces;

namespace Validator;

public class ValidatorHandler : IValidator
{
    private readonly List<IValidator> _validators;

    public ValidatorHandler(IEnumerable<IValidator> validators)
    {
        _validators = validators.ToList();
    }

    public Response Validate(string password)
    {
        var errorMessages = new List<string>();

        foreach (var validator in _validators)
        {
            var response = validator.Validate(password);
            if (!response.IsValid)
            {
                errorMessages.Add(response.Message);
            }
        }

        if (errorMessages.Any())
        {
            return new Response(false, string.Join(Environment.NewLine, errorMessages));
        }

        return new Response(true, string.Empty);
    }
}