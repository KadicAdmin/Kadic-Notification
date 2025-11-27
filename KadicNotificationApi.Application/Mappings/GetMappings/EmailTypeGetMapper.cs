using KadicNotificationApi.Application.DTOs.GetDto;
using KadicNotificationApi.Domain.Entities;
using KadicTechnology.CommonLib.Paginator;

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
        public static PaginatorResponseDto<EmailTypeGetDto> ToDto(this PaginatorResponseDto<EmailTemplateType> entidad)
        {
            return new PaginatorResponseDto<EmailTypeGetDto>
            {
                Data = entidad.Data.Select(s => s.ToGetDto()),
                TotalRecords = entidad.TotalRecords
            };
        }
    }
}