using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.UpdateMappings
{
    public static class EmailTemplateUpdateMapper
    {
        public static EmailTemplate ToEntity(this EmailTemplateUpdateDto emailTemplateUpdateDto)
        {
            return new EmailTemplate
            {
                Id = emailTemplateUpdateDto.Id,
                Subject = emailTemplateUpdateDto.Subject,
                TemplateTypeId = emailTemplateUpdateDto.TemplateTypeId,
                TenantId = emailTemplateUpdateDto.TenantId
            };
        }
    }
}