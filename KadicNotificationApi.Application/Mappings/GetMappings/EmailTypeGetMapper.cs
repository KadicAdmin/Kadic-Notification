using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.GetMappings
{
    public static class EmailTypeGetMapper
    {      
        public static EmailTypeGetDto ToGetDto(this EmailTemplateType entity)
        {
            return new EmailTypeGetDto
            {
                Id = entity.Id,
                HtmlBody = entity.HtmlBody

            };
        }
    }
}