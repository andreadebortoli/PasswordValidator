namespace Validator.Interfaces;

public interface IResponseBuilder
{
    ResponseBuilder ValidateLength(string password);
    ResponseBuilder ValidateDigits(string password);
    ResponseBuilder ValidateSpecialCharacters(string password);
    Response Build();
}