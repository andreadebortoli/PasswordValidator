using Validator.Interfaces;

namespace Validator;

public class LenghtValidator : IValidator 
{
    public Response Validate(string? password)
    {
        if (password is null)
        {
            return new Response
            {
                IsValid = false,
                Message = "Password cannot be null"
            };
        }

        if (password.Length < 8)
        {
            return new Response()
            {
                IsValid = false,
                Message = "Password must be at least 8 characters"
            };
        }

        return new Response()
        {
            IsValid = true,
            Message = string.Empty
        };
    }
}