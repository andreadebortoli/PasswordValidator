namespace Validator.Interfaces
{
    public interface IValidator
    {
        public Response Validate(string? password);
    }
}
