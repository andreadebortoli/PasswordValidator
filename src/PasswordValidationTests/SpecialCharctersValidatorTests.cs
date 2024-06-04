using Validator;

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

        Assert.False(response.IsValid);
    }
}