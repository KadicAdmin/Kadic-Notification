using Hangfire;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KadicNotificationApi.API.Controllers
{
    [ApiController]
    [Route("api/email")]
    public class EmailController : ControllerBase
    {       
        private readonly IBackgroundJobClient _backgroundJobClient;

        public EmailController(ISendTemplateService service, IBackgroundJobClient backgroundJobClient)
        {
          
            _backgroundJobClient = backgroundJobClient;
        }

        [HttpPost("send-by-template")]
        public IActionResult SendByTemplate([FromBody] SendTemplateSaveDto dto)
        {
          
            _backgroundJobClient.Enqueue<ISendTemplateService>(s => s.SendAsync(dto));

            return Ok(new { message = "Correo en cola para enviar." });
        }

        [HttpPost("schedule")]
        public IActionResult Schedule([FromBody] SendTemplateSaveDto dto)
        {            
            _backgroundJobClient.Schedule<ISendTemplateService>(s => s.SendAsync(dto), TimeSpan.FromMinutes(1));

            return Ok(new { message = "Correo en cola para enviar." });
        }
    }
}