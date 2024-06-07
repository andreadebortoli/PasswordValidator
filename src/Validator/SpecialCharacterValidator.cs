using Validator.Interfaces;

namespace Validator;

public class SpecialCharacterValidator : IValidator
{
    public Response Validate(string password)
    {
        if (password is not null && password.Any(char.IsPunctuation) )
        {
            return new Response(
                true,
                string.Empty
            );
        }

        return new Response(
            false,
            $"password must contain at least one special character"
        );
    }
}