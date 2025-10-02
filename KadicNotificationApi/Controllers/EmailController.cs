using FluentValidation;
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


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
}