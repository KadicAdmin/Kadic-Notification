using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.GetMappings;

public static class EmailTemplateGetMapper
{
    public static IEnumerable<EmailTemplateGetDto> ToDto(this IEnumerable<EmailTemplate> entity)
    {
        return entity.Select(s => s.ToGetDto());
    }
    public static EmailTemplateGetDto ToGetDto(this EmailTemplate entity)
    {
        return new EmailTemplateGetDto
        {
            Id = entity.Id,
            Subject = entity.Subject,
            TemplateTypeId = entity.TemplateTypeId,
            TenantId = entity.TenantId
        };
    }    
}