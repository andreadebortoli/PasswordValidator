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

        Assert.False(response.IsValid);
    }

}