namespace Validator.Interfaces
{
    public interface IValidator
    {
        Response Validate(string? password);
    }
}
