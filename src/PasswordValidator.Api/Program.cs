using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Validator;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IValidator>(sp => new ValidatorHandler(new List<IValidator>()
    {
        new LengthValidator(),
        new TwoNumbersValidator(),
        new SpecialCharacterValidator(),
        new CapitalLetterValidator()
    }
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/validator", (
        [FromServices] IValidator validator,
        string password) =>
    {
        var result = validator.Validate(password);

        return Results.Ok(result);
    })
.WithName("PasswordValidator")
.WithOpenApi();

app.Run();

