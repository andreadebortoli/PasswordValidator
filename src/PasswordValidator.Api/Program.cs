using Common;
using FileHandler;
using Microsoft.AspNetCore.Mvc;
using Validator.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InizializeValidator();

builder.Services.AddSingleton<IWriter, FileWriter>();

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
        [FromServices] IWriter fileHandler,
        string password) =>
    {
        var result = validator.Validate(password);

        if (result.IsValid)
        {
            EncryptionHandler.EncryptionHandler.EncryptPassword(password);
            fileHandler.WriteToFile(password);
            return Results.Ok(result);
        }

        return Results.BadRequest(result);
    })
.WithName("PasswordValidator")
.WithOpenApi();

app.Run();

