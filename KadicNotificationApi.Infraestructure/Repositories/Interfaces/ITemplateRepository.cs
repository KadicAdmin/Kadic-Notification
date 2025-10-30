using KadicNotificationApi.Infraestructure.Persistence.Repositories;

namespace KadicNotificationApi.Infraestructure.Repositories.Interfaces
{
    public interface ITemplateRepository
    {
        Task<EmailTemplateEntity> GetByIdAsync(int templateId);
    }
}