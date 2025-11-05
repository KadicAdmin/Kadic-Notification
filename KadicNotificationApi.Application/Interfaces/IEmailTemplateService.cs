using KadicNotificationApi.Application.DTOs;

namespace KadicNotificationApi.Application.Interfaces
{
    public interface IEmailTemplateService
    {
        Task SendById(EmailTemplateDto emailTemplateDto);
    }
}