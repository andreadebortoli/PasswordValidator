using Validator.Interfaces;

namespace Validator.Validators;

public class TwoNumbersValidator : IValidator
{
    public Response Validate(string password)
    {
        if (password is not null && password.Count(char.IsDigit) >= 2)
        {
            return new Response(
                true,
                string.Empty
            );
        }

        return new Response(
                false,
                $"The password must contain at least 2 numbers")
            ;
    }
}