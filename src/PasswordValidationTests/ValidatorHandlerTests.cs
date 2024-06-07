using Validator;
using Validator.Interfaces;

namespace PasswordValidationTests;

public class ValidatorHandlerTests
{
    private IValidator _sut;
    private IEnumerable<IValidator> _validators;

    [SetUp]
    public void SetUp()
    {
        _validators = new List<IValidator>
        {
            new LengthValidator(),
            new TwoNumbersValidator(),
            new SpecialCharacterValidator(),
            new CapitalLetterValidator()
        };

        _sut = new ValidatorHandler(_validators);
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



}