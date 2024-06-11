using Validator;
using Validator.Interfaces;
using Validator.Validators;

namespace PasswordValidationTests;

public class SpecialCharctersValidatorTests
{
    private IValidator _sut;

    [SetUp]
    public void SetUp()
    {
        _sut = new SpecialCharacterValidator();
    }

    [Test]
    [TestCase("Password!")]
    public void PasswordMustContainsAtLeastOneSpecialCharacters(string password)
    {
        var response = _sut.Validate(password);

        Assert.True(response.IsValid);
    }

    [Test]
    [TestCase("Pass_w@rd!")]
    public void ReturnsValidIfContainsMoreThanOneSpecialCharacters(string password)
    {
        var response = _sut.Validate(password);

        Assert.True(response.IsValid);
    }

    [Test]
    [TestCase("Password")]
    public void ReturnsInvalidIfDoesntContainsSpecialCharacters(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "password must contain at least one special character"
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
            "password must contain at least one special character"
        );

        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }
}