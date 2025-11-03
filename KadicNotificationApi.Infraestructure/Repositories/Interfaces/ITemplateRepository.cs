using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        Task<EmailTemplateEntity> GetByIdAsync(int templateId);
    }
}