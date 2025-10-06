using FluentValidation;
using FluentValidation.AspNetCore; 
using Hangfire;
using Hangfire.SqlServer;
using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Application.Services;
using KadicNotificationApi.Infraestructure.Config;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers();

// Settings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

// FluentValidation
builder.Services
    .AddFluentValidationAutoValidation(); 
  

// Servicios de la app
builder.Services.AddScoped<IEmailSender, SendEmailService>();

// Swagger (antes de Build)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Hangfire
var connectionString = builder.Configuration.GetConnectionString("SourcQL1002.site4now.net;Initial Catalog=db_abb04a_kadicnotification;User Id=db_abb04a_kadicnotification_admin;Password=K@dicTech2025e=S");
builder.Services.AddHangfire(cfg =>
{
    cfg.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
       {
           CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
           SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
           QueuePollInterval = TimeSpan.Zero,
           UseRecommendedIsolationLevel = true,
           DisableGlobalLocks = true
       });
});
builder.Services.AddHangfireServer();

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI();

// (Opcional) Dashboard de Hangfire
app.UseHangfireDashboard(); 

app.MapControllers();
app.Run();
