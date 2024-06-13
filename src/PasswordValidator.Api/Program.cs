using Common;
using FileHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PasswordValidator.Api;
using Validator;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<FileHandlerOptions>(builder.Configuration.GetSection("FileHandler"));

builder.Services.InizializeValidator();

builder.Services.AddSingleton<IWriter>(sp =>
{
    var options = sp.GetRequiredService<IOptions<FileHandlerOptions>>();
    return new FileWriter(options.Value.Path, options.Value.FileName);
});
builder.Services.AddSingleton<IPasswordEncryptor, PasswordEncryptor>();
builder.Services.AddScoped<IPasswordChecker, PasswordChecker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/validator", (
        [FromServices] IValidatorHandler validator,
        [FromServices] IPasswordChecker passwordChecker,
        string password) =>
    {
        var response = validator.Validate(password);

        passwordChecker.CheckPassword(response.IsValid, password);

        return Results.Ok(response);
    })
.WithName("PasswordValidator")
.WithOpenApi();

app.Run();

