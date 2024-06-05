using Validator;
using Validator.Interfaces;

namespace PasswordValidationTests;

public class LenghtValidatorTests
{

    private IValidator _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new LengthValidator();
    }

    [Test]
    public void PasswordMustBeAtLeastEightCharactersLong()
    {
        var response = _sut.Validate("Password");

        Assert.True(response.IsValid);
    }

    [Test]
    public void ReturnInvalidIfPasswordHasMoreThanEightCharacters()
    {
        var response = _sut.Validate("Password123");

        Assert.True(response.IsValid);
    }

    [Test]
    public void PasswordShouldntBeShorterThanEightCharactersLong()
    {
        var response = _sut.Validate("Passwo");

        var invalidPasswordResponse = new Response()
        {
            IsValid = false,
            Message = "Password must be at least 8 characters"
        };
        Assert.That(invalidPasswordResponse.IsValid, Is.EqualTo(response.IsValid));
        Assert.That(invalidPasswordResponse.Message, Is.EqualTo(response.Message));
    }

    [Test]
    public void PasswordShouldntBeEmpyString()
    {
        var response = _sut.Validate(string.Empty);

        var invalidPasswordResponse = new Response()
        {
            IsValid = false,
            Message = "Password must be at least 8 characters"
        };

        Assert.That(invalidPasswordResponse.IsValid, Is.EqualTo(response.IsValid));
        Assert.That(invalidPasswordResponse.Message, Is.EqualTo(response.Message));
    }

    [Test]
    public void PasswordShouldntBeNull()
    {
        var response = _sut.Validate(null);

        var invalidPasswordResponse = new Response()
        {
            IsValid = false,
            Message = "Password cannot be null"
        };

        Assert.That(invalidPasswordResponse.IsValid, Is.EqualTo(response.IsValid));
        Assert.That(invalidPasswordResponse.Message, Is.EqualTo(response.Message));
    }
}