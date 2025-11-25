using KadicNotificationApi.Application.DTOs.DeleteDto;
using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicTechnology.CommonLib.Paginator;
using KadicTechnology.CommonLib.Utils;

namespace KadicNotificationApi.Application.Services.Interfaces
{
    public interface IEmailTemplateSerivice
    {
        public Task<Result<EmailTemplateGetDto>> GetById (int id, int tenantId);
        public Task<Result<PaginatorResponseDto<EmailTemplateGetDto>>> GetByTenant(int tenantId, PaginatorRequestDto paginatorRequestDto);
        public Task<Result> Save(EmailTemplateSaveDto emailTemplateSaveDto );
        public Task<Result> Update(EmailTemplateUpdateDto emailTemplateUpdateDto);
        public Task<Result> Delete(EmailTemplateDeleteDto emailTemplateDeleteDto);
    }
}