using System.Text.RegularExpressions;

namespace Validator;

public class SpecialCharacterValidator : IValidator
{
    public Response Validate(string? password)
    {
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