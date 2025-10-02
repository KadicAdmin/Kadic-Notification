using KadicNotificationApi.Application.Validators;
using KadicNotificationApi.Infraestructure.Config;
using FluentValidation;
using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Application.Services;

var builder = WebApplication.CreateBuilder(args);

//Controllers
builder.Services.AddControllers();

//Settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<SendEmailCommandValidator>();

//Services
builder.Services.AddScoped<IEmailSender, SendEmailService>();

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();