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
    [TestCase("Password12")]
    [TestCase("Password123")]
    public void PasswordMustContainsAtLEastTwoNumbers(string password)
    {
        var response = _sut.Validate(password);

        Assert.True(response.IsValid);
    }

    [Test]
    [TestCase("Password1")]
    public void ReturnsInvalidIfContainsLessThanTwoNumbers(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "The password must contain at least 2 numbers"
        );

        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void PasswordShouldntBeEmpyStringOrNull(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "The password must contain at least 2 numbers"
        );

        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }
}