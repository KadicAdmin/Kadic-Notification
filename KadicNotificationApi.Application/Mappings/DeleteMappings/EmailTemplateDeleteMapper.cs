using KadicNotificationApi.Application.DTOs.DeleteDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.DeleteMappings
{
    public static class EmailTemplateDeleteMapper
    {
        public static EmailTemplate ToEntity(this EmailTemplateDeleteDto emailTemplateDeleteDto)
        {
            return new EmailTemplate
            {
                Id = emailTemplateDeleteDto.Id,                
                TenantId = emailTemplateDeleteDto.TenantId              
            };
        }
    }
}