using KadicNotificationApi.Domain.Entities;
using KadicTechnology.CommonLib.Paginator;

namespace KadicNotificationApi.Infraestructure.Repository.Interface

{
    public interface IEmailTypeRepository
    {
        public Task<EmailTemplateType?> GetByIdAsync(int id);
        public Task<PaginatorResponseDto<EmailTemplateType>> GetAllAsync(PaginatorRequestDto paginatorRequestDto);
        public Task SaveAsync(EmailTemplateType emailTemplateType);
        public Task UpdateAsync(EmailTemplateType template);
        public Task DeleteAsync(int id);
    }
}