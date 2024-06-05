using Microsoft.AspNetCore.Mvc;
using Validator;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<Dictionary<string, IValidator>>(sp => new Dictionary<string, IValidator>
{
    { "length", new LengthValidator() },
    { "twoNumbers", new TwoNumbersValidator() },
    { "specialCharacters", new SpecialCharacterValidator() }
});


builder.Services.AddScoped<IValidatorFactory, ValidatorFactory>();


builder.Services.AddScoped<IResponseBuilder, ResponseBuilder>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/validator", (
        [FromServices] IResponseBuilder responseBuilder,
        string password) =>
    {
        var result = responseBuilder
            .ValidateLength(password)
            .ValidateDigits(password)
            .ValidateSpecialCharacters(password)
            .Build();


        return Results.Ok(result);
    })
.WithName("PasswordValidator")
.WithOpenApi();

app.Run();
