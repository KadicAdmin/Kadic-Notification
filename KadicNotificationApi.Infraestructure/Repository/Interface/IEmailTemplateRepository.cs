using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Repository.Interface
{
    public interface IEmailTemplateRepository
    {
        public Task<EmailTemplate?> GetByIdAsync(int id, int tenantId);
        public Task<IReadOnlyList<EmailTemplate>> GetByTenantAsync(int tenantId, int page, int pageSize);
        public Task UpdateAsync(EmailTemplate template);
        public Task DeleteAsync(EmailTemplate template);
        public Task SaveAsync(EmailTemplate template);
    }
}