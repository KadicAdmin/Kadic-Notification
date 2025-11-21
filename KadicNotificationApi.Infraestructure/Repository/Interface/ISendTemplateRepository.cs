using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Repository.Interface
{
    public interface ISendTemplateRepository
    {
        Task<EmailTemplate?> GetByIdAsync(int id);
    }
}