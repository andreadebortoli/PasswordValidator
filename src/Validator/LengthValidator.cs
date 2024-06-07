using Validator.Interfaces;

namespace Validator;

public class LengthValidator : IValidator
{
    public Response Validate(string? password)
    {
        if (password is not null && password.Length > 7)
        {
            return new Response(
                 true,
            string.Empty
            );
        }

        return new Response(
           false,
           "Password must be at least 8 characters")
        ;
    }
}