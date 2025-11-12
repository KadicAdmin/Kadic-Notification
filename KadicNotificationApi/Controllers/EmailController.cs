
using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KadicNotificationApi.API.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailTemplateService _service;

        public EmailController(IEmailTemplateService service)
        {
            _service = service;
        }

        [HttpPost("send-by-template")]
        public async Task<IActionResult> SendByTemplate([FromBody] SendByTemplateRequestDto dto)
        {
            await _service.SendAsync(dto);
            return Ok(new { message = "Correo enviado." });
        }
    }
}