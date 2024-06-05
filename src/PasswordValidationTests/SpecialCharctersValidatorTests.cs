using Validator;
using Validator.Interfaces;

namespace PasswordValidationTests;

public class SpecialCharctersValidatorTests
{
    private IValidator _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new SpecialCharacterValidator();
    }

    [Test]
    public void PasswordMustContainsAtLeastOneSpecialCharacters()
    {
        var response = _sut.Validate("Password!");

        Assert.True(response.IsValid);
    }

    [Test]
    public void ReturnsValidIfContainsMoreThanOneSpecialCharacters()
    {
        var response = _sut.Validate("Pass_w@rd!");

        Assert.True(response.IsValid);
    }

    [Test]
    public void ReturnsInvalidIfDoesntContainsSpecialCharacters()
    {
        var response = _sut.Validate("Password");

        var invalidPasswordResponse = new Response()
        {
            IsValid = false,
            Message = "password must contain at least one special character"
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
            Message = "password must contain at least one special character"
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