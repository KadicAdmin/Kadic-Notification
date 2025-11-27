using FluentValidation;
using FluentValidation.AspNetCore;
using Hangfire;
using Hangfire.SqlServer;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.DTOs.DeleteDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Application.Services.Implementation;
using KadicNotificationApi.Application.Services.Interfaces;
using KadicNotificationApi.Application.Validators.EmailTemplateValidators;
using KadicNotificationApi.Application.Validators.EmailTypeValidators;
using KadicNotificationApi.Application.Validators.SendEmailValidators;
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
builder.Services.AddDbContext<NotificationDbContext>
    (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<SendTemplateValidator>();

// Validators//

//EmailTemplate
builder.Services.AddScoped<IValidator<EmailTemplateSaveDto>, EmailTemplateSaveDtoValidator>();
builder.Services.AddScoped<IValidator<EmailTemplateUpdateDto>, EmailTemplateUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<EmailTemplateDeleteDto>, EmailTemplateDeleteDtoValidator>();

//EmailType
builder.Services.AddScoped<IValidator<EmailTypeSaveDto>, EmailTypeSaveValidator>();
builder.Services.AddScoped<IValidator<EmailTypeUpdateDto>, EmailTypeUpdateValidator>();
builder.Services.AddScoped<IValidator<EmailTypeDeleteDto>, EmailTypeDeleteValidator>();


// Servicios
builder.Services.AddScoped<ISendTemplateService, SendTemplateService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<IEmailTypeService, EmailTypeService>();

// Repository
builder.Services.AddScoped<ISendTemplateRepository, SendTemplateRepository>();
builder.Services.AddScoped<SmtpEmailTemplate>();
builder.Services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
builder.Services.AddScoped<IEmailTypeRepository, EmailTypeRepository>();

// Swagger (antes de Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//HangFire
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddHangfire(Configuration => Configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
        {
            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
            QueuePollInterval = TimeSpan.Zero,
            UseRecommendedIsolationLevel = true,
            DisableGlobalLocks = true
        }));
builder.Services.AddHangfireServer(options => options.SchedulePollingInterval = TimeSpan.FromMinutes(1));


var app = builder.Build();



// Swagger
app.UseHangfireDashboard();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();