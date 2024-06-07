using Microsoft.AspNetCore.Mvc;
using Validator;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LengthValidator>();
builder.Services.AddScoped<TwoNumbersValidator>();
builder.Services.AddScoped<SpecialCharacterValidator>();
builder.Services.AddScoped<CapitalLetterValidator>();

builder.Services.AddScoped<IValidator>(sp => new ValidatorHandler(new List<IValidator>()
    {
        sp.GetRequiredService<LengthValidator>(),
        sp.GetRequiredService<TwoNumbersValidator>(),
        sp.GetRequiredService<SpecialCharacterValidator>(),
        sp.GetRequiredService<CapitalLetterValidator>(),
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

