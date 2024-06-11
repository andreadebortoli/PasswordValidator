using Validator.Interfaces;
namespace Validator;

public class ValidatorsHandler : IValidatorHandler
{
    private readonly IEnumerable<IValidator> _validators;

    public ValidatorsHandler(IEnumerable<IValidator> validators)
    {
        _validators = validators;
    }

    public Response Validate(string password)
    {
        var errorMessages = new List<string>();

        foreach (var validator in _validators)
        {
            var response = validator.Validate(password);
            if (!response.IsValid)
            {
                errorMessages.Add(response.Message!);
            }
        }

        return errorMessages.Any()
            ? new Response(false, string.Join(Environment.NewLine, errorMessages))
            : new Response(true, string.Empty);
    }
}