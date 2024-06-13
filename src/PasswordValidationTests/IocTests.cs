using Common;
using Microsoft.Extensions.DependencyInjection;
using Validator.Interfaces;
using Validator.Validators;

namespace PasswordValidationTests;

class IocTests
{
    private IServiceCollection _serviceCollection;
    private IServiceProvider _serviceProvider;

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = new ServiceCollection();
        _serviceCollection.InizializeValidator();
        _serviceProvider = _serviceCollection.BuildServiceProvider();
    }

    [Test]
    public void Should_register_All_Validators()
    {
        var validators = _serviceProvider.GetServices<IValidator>();

        Assert.IsNotNull(validators.Where(v => v.Equals(typeof(LengthValidator))));
        Assert.IsNotNull(validators.Where(v => v.Equals(typeof(TwoNumbersValidator))));
        Assert.IsNotNull(validators.Where(v => v.Equals(typeof(SpecialCharacterValidator))));
        Assert.IsNotNull(validators.Where(v => v.Equals(typeof(CapitalLetterValidator))));
    }

    [Test]
    public void Should_register_ValidatorHandler()
    {
        var handler = _serviceProvider.GetService<IValidatorHandler>();
        Assert.IsNotNull(handler);
    }

}