using Validator;
using Validator.Interfaces;

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
    }
}
