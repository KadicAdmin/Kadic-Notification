using KadicNotificationApi.Application.DTOs.UpdateDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.UpdateMappings
{
    public static class EmailTypeUpdateMapper
    {
        public static EmailTemplateType ToEntity(this EmailTypeUpdateDto emailTemplateType)
        {
            return new EmailTemplateType
            {
                Id = emailTemplateType.Id,
                HtmlBody = emailTemplateType.HtmlBody
            };
        }
    }
}