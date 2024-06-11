using Microsoft.Extensions.DependencyInjection;
using Validator;
using Validator.Interfaces;
using Validator.Validators;

namespace Common;

public static class Container
{
    public static void InizializeValidator(this IServiceCollection services)
    {
        services.AddScoped<IValidator, LengthValidator>();
        services.AddScoped<IValidator, TwoNumbersValidator>();
        services.AddScoped<IValidator, SpecialCharacterValidator>();
        services.AddScoped<IValidator, CapitalLetterValidator>();

        services.AddScoped<IValidatorHandler, ValidatorsHandler>();
    }
}