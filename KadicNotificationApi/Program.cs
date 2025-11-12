using FluentValidation;
using FluentValidation.AspNetCore;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Services.Implementation;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Application.Validators;
using KadicNotificationApi.Infraestructure.Config;
using KadicNotificationApi.Infraestructure.DbContexts;
using KadicNotificationApi.Infraestructure.Repository.Implementation;
using KadicNotificationApi.Infraestructure.Repository.Interface;
using KadicNotificationApi.Infraestructure.SMTP;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Settings
builder.Services.AddOptions<EmailSettings>()
    .Bind(builder.Configuration.GetSection("EmailSettings"))
    .ValidateOnStart();

// DbContext
builder.Services.AddDbContext<NotificationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SendByTemplateRequestValidator>();

// Servicios
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();

// Repository
builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddScoped<SmtpEmailTemplate>();

// Swagger (antes de Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();


// Swagger
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();