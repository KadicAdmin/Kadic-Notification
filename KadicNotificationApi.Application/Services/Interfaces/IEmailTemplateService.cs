using KadicNotificationApi.Application.DTOs;

namespace KadicNotificationApi.Application.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task SendAsync(SendByTemplateRequestDto sendByTemplateRequestDto);
    }
}