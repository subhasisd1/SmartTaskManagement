using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using SmartTaskManagement.API.Extensions;
using SmartTaskManagement.Application;
using SmartTaskManagement.Domain.Entities;
using SmartTaskManagement.Persistence;
using SmartTaskManagement.Persistence.Contexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidatorsFromAssembly(typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);
builder.Services.AddAutoMapper(typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);

// Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

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