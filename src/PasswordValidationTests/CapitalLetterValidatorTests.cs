using Validator;
using Validator.Interfaces;
using Validator.Validators;

namespace PasswordValidationTests
{
    public class CapitalLetterValidatorTests
    {

        private IValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CapitalLetterValidator();
        }

        [Test]
        [TestCase("Password")]
        [TestCase("pASsword")]
        public void PasswordMustHaveAtLeastOneCapitalLetter(string password)
        {
            var response = _sut.Validate(password);

            Assert.IsTrue(response.IsValid);
        }

        [Test]
        [TestCase("password")]
        public void ReturnsInvalidIfPasswordHasLessThanOneCapitalLetter(string password)
        {
            var response = _sut.Validate(password);

            var invalidResponse = new Response(
                false,
                "password must contain at least one capital letter"
            );

            Assert.That(invalidResponse, Is.EqualTo(response));
        }

        [Test]
        [TestCase("")]
        [TestCase(null)]
        public void PasswordShouldntBeEmpyStringOrNull(string password)
        {
            var response = _sut.Validate(password);

            var invalidPasswordResponse = new Response(
                false,
                "password must contain at least one capital letter"
            );

            Assert.That(invalidPasswordResponse, Is.EqualTo(response));
        }
    }
}
