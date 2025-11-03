using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Application.Services;
using KadicNotificationApi.Infraestructure.Config;
using FluentValidation;
using FluentValidation.AspNetCore;
using KadicNotificationApi.Application.Validators;
using KadicNotificationApi.Application.DTOs.GetDto;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();

//  Registro de validators 
builder.Services.AddValidatorsFromAssemblyContaining<SendEmailRequestValidator>();
builder.Services.AddScoped<IValidator<SendEmailRequest>, SendEmailRequestValidator>();

// Servicios de la app
builder.Services.AddScoped<IEmailSender, SendEmailService>();

// Swagger (antes de Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();