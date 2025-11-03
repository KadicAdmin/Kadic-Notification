using FluentValidation;
using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Crmf;

namespace KadicNotificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IEmailSender _emailSender;
    private readonly IValidator<SendEmailRequest> _validator;


    public NotificationController(IEmailSender sender, IValidator<SendEmailRequest> validator)
    {
        _emailSender = sender;
        _validator = validator;
    }

    [HttpPost("SendEmail")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> SendEmail([FromBody] SendEmailRequest request)
    {
        var result = await _validator.ValidateAsync(request);

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
        await _emailSender.SendAsync(request);
        return Ok(new { message = "Correo enviado con éxito." });
    }

    //[HttpGet("{id}")]
    //public async Task 
}