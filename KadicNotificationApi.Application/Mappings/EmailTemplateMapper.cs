using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings
{
    public static class EmailTemplateMapper
    {
        public static EmailTemplateDto ToDto(this EmailTemplate emailTemplate )
        {
            return new EmailTemplateDto
            {
                Subjet = emailTemplate.Subjet,
                HtmlBody = emailTemplate.Type?.HtmlBody ?? ""   
            };
        }
    }
}