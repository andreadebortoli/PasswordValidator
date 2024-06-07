using Validator.Interfaces;

namespace Validator;

public class CapitalLetterValidator : IValidator
{
    public Response Validate(string password)
    {
        if (password is not null && password.Any(char.IsUpper))
        {
            return new Response(
                true,
                string.Empty
            );
        }

        return new Response(
            false,
            $"password must contain at least one capital letter"
        );
    }
}