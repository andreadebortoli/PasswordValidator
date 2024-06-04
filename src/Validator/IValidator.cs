namespace Validator
{
    public interface IValidator
    {
        Response Validate(string? password);
    }
}
