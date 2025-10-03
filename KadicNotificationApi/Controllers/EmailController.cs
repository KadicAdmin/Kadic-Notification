using FluentValidation;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Interfaces;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Security.Authentication;


namespace KadicNotificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IValidator<SendEmailRequest> _validator;


    public EmailController(IEmailSender sender, IValidator<SendEmailRequest> validator)
    {
        _emailSender = sender;
        _validator = validator;
    }

    [HttpPost("send")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request, CancellationToken cancellation)
    {
        var result = await _validator.ValidateAsync(request, cancellation);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                 h => h.Key,
                 h => h.Select(x => x.ErrorMessage).ToArray()
                );

            return ValidationProblem(new ValidationProblemDetails(errors));
        }
        await _emailSender.SendAsync(request, cancellation);
        return Ok(new { message = "Correo enviado con éxito." });
    }

    [HttpPost("TestSmtp")]
    public async Task<IActionResult> TestSmtp()
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse("admin@kadictechnology.com"));
        message.To.Add(MailboxAddress.Parse("ricardo.devsoftware@gmail.com"));
        message.Subject = "Prueba SMTP";
        message.Body = new TextPart("plain") { Text = "Hola, prueba de SMTP GoDaddy" };

        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.office365.com", 587, SecureSocketOptions.StartTls);
        client.AuthenticationMechanisms.Remove("XOAUTH2");

        await client.AuthenticateAsync("admin@kadictechnology.com", "IWillNeverForgetIt!!2025");
        await client.SendAsync(message);
        await client.DisconnectAsync(true);

        return Ok();
    }    
}