using FluentValidation;
using Microsoft.OpenApi.Models;
using SmartTaskManagement.API.Extensions;
using SmartTaskManagement.Application;
using SmartTaskManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidatorsFromAssembly(typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);
builder.Services.AddAutoMapper(typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);



// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Smart Task Management API",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Ensure Swashbuckle.AspNetCore package is installed
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthorization();

app.MapControllers();

app.Run();