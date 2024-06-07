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
    [TestCase("password")]
    [TestCase("Password123")]
    public void PasswordMustBeAtLeastEightCharactersLong(string password)
    {
        var response = _sut.Validate(password);

        Assert.True(response.IsValid);
    }

    [Test]
    [TestCase("passwo")]
    public void PasswordShouldntBeShorterThanEightCharactersLong(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "Password must be at least 8 characters"
        );
        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void PasswordShouldntBeEmpyString(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "Password must be at least 8 characters"
        );

        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }
}