using Common;
using Microsoft.Extensions.DependencyInjection;
using Validator;

namespace PasswordValidationTests;

public class ValidatorHandlerTests
{
    private IValidatorHandler _sut;
    private IServiceCollection _services;
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void SetUp()
    {
        _services = new ServiceCollection();
        _services.InizializeValidator();
        _serviceProvider = _services.BuildServiceProvider();
        _sut = _serviceProvider.GetService<IValidatorHandler>();
    }

    [Test]
    [TestCase("Password_12@")]
    [TestCase("Password_12@345")]
    public void HandlerShouldReturnsIsValidTrueIfAllValidatorsReturnsTrue(string password)
    {
        var response = _sut.Validate(password);

        Assert.True(response.IsValid);
    }

    [Test]
    [TestCase("")]
    [TestCase(null)]
    public void PasswordShouldntBeEmpyStringOrNull(string password)
    {
        var response = _sut.Validate(password);

        var invalidPasswordResponse = new Response(
            false,
            "Password must be at least 8 characters\r\nThe password must contain at least 2 numbers\r\npassword must contain at least one special character\r\npassword must contain at least one capital letter"
        );

        Assert.That(invalidPasswordResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("passwo")]
    public void HandlerShouldReturnsIsValidFalseAndAllErrorsIfAllValidatorsDontPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\nThe password must contain at least 2 numbers\r\npassword must contain at least one special character\r\npassword must contain at least one capital letter"
        );

        Assert.That(invalidResponse , Is.EqualTo(response));
    }

    [Test]
    [TestCase("password")]
    public void HandlerShouldReturnsIsValidFalseAndAllErrorsExceptLenghtErrorIfOnlyLengthValidatorPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "The password must contain at least 2 numbers\r\npassword must contain at least one special character\r\npassword must contain at least one capital letter"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("pass12")]
    public void HandlerShouldReturnsIsValidFalseAndAllErrorsExceptTwoNumbersIfOnlyTwoNumbersValidatorPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\npassword must contain at least one special character\r\npassword must contain at least one capital letter"

        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("pass@")]
    public void HandlerShouldReturnsIsValidFalseAndAllErrorsExceptSpecialCharactersIfOnlySpecialCharactersValidatorPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\nThe password must contain at least 2 numbers\r\npassword must contain at least one capital letter"


        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("Pass")]
    public void HandlerShouldReturnsIsValidFalseAndAllErrorsExceptCapitalLetterIfOnlyCapitalLetterValidatorPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\nThe password must contain at least 2 numbers\r\npassword must contain at least one special character"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }


    [Test]
    [TestCase("password12")]
    public void HandlerShouldReturnsIsValidFalseAndSpecialCharactersErrorAndCapitalLetterErrorIfOnlyLengthAndTwoNumbersValidatorsPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "password must contain at least one special character\r\npassword must contain at least one capital letter"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("passwO_")]
    public void HandlerShouldReturnsIsValidFalseAndLengthErrorAndTwoNumbersErrorIfOnlyLenghtAndTwoNumbersValidatorsPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\nThe password must contain at least 2 numbers"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("Password")]
    public void HandlerShouldReturnsIsValidFalseAndAndTwoNumbersErrorAndSpecialCharacterErrorIfOnlyLengthAndCapitalLetterValidatorsPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "The password must contain at least 2 numbers\r\npassword must contain at least one special character"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }

    [Test]
    [TestCase("pass23!")]
    public void HandlerShouldReturnsIsValidFalseAndAndLengthErrorAndCapitalLetterErrorIfOnlyTwoNumbersAndSpecialCharactersValidatorsPass(string password)
    {
        var response = _sut.Validate(password);

        var invalidResponse = new Response(
            false,
            "Password must be at least 8 characters\r\npassword must contain at least one capital letter"
        );

        Assert.That(invalidResponse, Is.EqualTo(response));
    }



}