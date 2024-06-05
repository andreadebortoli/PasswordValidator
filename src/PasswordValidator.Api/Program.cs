using Microsoft.AspNetCore.Mvc;
using Validator;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddKeyedScoped<IValidator, LenghtValidator>("length");
builder.Services.AddKeyedScoped<IValidator, TwoNumbersValidator>("twoNumbers");
builder.Services.AddKeyedScoped<IValidator, SpecialCharacterValidator>("specialCharacters");


builder.Services.AddScoped<IResponseBuilder, ResponseBuilder>(serviceProvider =>
    new ResponseBuilder(
        serviceProvider.GetRequiredKeyedService<IValidator>("length"),
        serviceProvider.GetRequiredKeyedService<IValidator>("twoNumbers"),
        serviceProvider.GetRequiredKeyedService<IValidator>("specialCharacters")));

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

