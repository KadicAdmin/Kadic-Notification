using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Repositories.Interfaces
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate> SendByIdAsync(int templateId);
    }
}