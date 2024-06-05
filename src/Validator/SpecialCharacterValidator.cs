using System.Text.RegularExpressions;
using Validator.Interfaces;

namespace Validator;

public class SpecialCharacterValidator : IValidator
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

        var pattern = @"[ -\/:-@\[-\`{-~]";

        var matches = Regex.Matches(password!, pattern);

        var specialCharactersCount = matches.Count;

        if (specialCharactersCount == 0)
        {
            return new Response()
            {
                IsValid = false,
                Message = "password must contain at least one special character"
            };
        }

        return new Response()
        {
            IsValid = true,
            Message = string.Empty
        };
    }
}