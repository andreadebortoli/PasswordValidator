using Validator.Interfaces;

namespace Validator;

public class TwoNumbersValidator : IValidator
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

        var numbersInPassword = password!.Where(c => char.IsDigit(c)).ToList();

        if (numbersInPassword.Count < 2)
        {
            return new Response()
            {
                IsValid = false,
                Message = "The password must contain at least 2 numbers"
            };
        }

        return new Response()
        {
            IsValid = true,
            Message = string.Empty
        };
    }
}