using Validator;
using Validator.Interfaces;

namespace PasswordValidationTests;

public class TwoNumbersValidatorTests
{
    private IValidator _sut;

    [SetUp]
    public void Setup()
    {
        _sut = new TwoNumbersValidator();
    }

    [Test]
    public void PasswordMustContainsAtLEastTwoNumbers()
    {
        var response = _sut.Validate("Password12");

        Assert.True(response.IsValid);
    }

    [Test]
    public void ReturnsValidIfContainsMoreThanTwoNumbers()
    {
        var response = _sut.Validate("Password123");

        Assert.True(response.IsValid);
    }

    [Test]
    public void ReturnsInvalidIfContainsLessThanTwoNumbers()
    {
        var response = _sut.Validate("Password1");

        var invalidPasswordResponse = new Response()
        {
            IsValid = false,
            Message = "The password must contain at least 2 numbers"
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
            Message = "The password must contain at least 2 numbers"
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