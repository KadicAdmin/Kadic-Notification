using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.SaveMappings
{
    public static class EmailTypeSaveMapper
    {
        public static EmailTemplateType ToEntity(this EmailTypeSaveDto emailTypeSaveDto)
        {
            return new EmailTemplateType
            {
                HtmlBody = emailTypeSaveDto.HtmlBody
            };
        }
    }
}