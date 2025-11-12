using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Repository.Interface
{
    public interface IEmailTemplateRepository
    {
        Task<EmailTemplate?> GetByIdAsync(int id);
    }
}