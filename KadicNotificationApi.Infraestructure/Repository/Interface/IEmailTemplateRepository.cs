using KadicNotificationApi.Domain.Entities;
using KadicTechnology.CommonLib.Paginator;

namespace KadicNotificationApi.Infraestructure.Repository.Interface
{
    public interface IEmailTemplateRepository
    {
        public Task<EmailTemplate?> GetByIdAsync(int id, int tenantId);
        public Task<PaginatorResponseDto<EmailTemplate>> GetByTenantAsync(int tenantId, PaginatorRequestDto paginatorRequestDto);
        public Task UpdateAsync(EmailTemplate template);
        public Task DeleteAsync(int id, int tenantId);
        public Task SaveAsync(EmailTemplate template);
    }
}