using FluentValidation;
using Microsoft.OpenApi.Models;
using SmartTaskManagement.API.Extensions;
using SmartTaskManagement.API.Services;
using SmartTaskManagement.Application;
using SmartTaskManagement.Application.DTOs;
using SmartTaskManagement.Application.Interfaces;
using SmartTaskManagement.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddValidatorsFromAssembly(
    typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);

builder.Services.AddAutoMapper(
    typeof(SmartTaskManagement.Application.DependencyInjection).Assembly);

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<
    ICurrentUserService,
    CurrentUserService>();

// Authentication + Identity + JWT
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerConfiguration();

// MVC
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();




var app = builder.Build();

app.UseSwaggerConfiguration();

app.UseHttpsRedirection();

app.UseGlobalExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();