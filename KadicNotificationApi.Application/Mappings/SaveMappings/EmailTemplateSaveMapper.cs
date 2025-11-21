using KadicNotificationApi.Application.DTOs.SaveDto;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Application.Mappings.SaveMappings
{
    public static class EmailTemplateSaveMapper
    {
      public static EmailTemplate ToEntity(this EmailTemplateSaveDto emailTemplateSaveDto)
      {
        return new EmailTemplate
        {
          Subject = emailTemplateSaveDto.Subjetc,
          TemplateTypeId = emailTemplateSaveDto.TemplateTypeId
        };
        }
    }
}
