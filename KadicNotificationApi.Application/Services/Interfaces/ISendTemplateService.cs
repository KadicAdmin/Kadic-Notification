using KadicNotificationApi.Application.DTOs.SaveDto;

namespace KadicNotificationApi.Application.Services.Interfaces
{
    public interface ISendTemplateService
    {
        Task SendAsync(SendTemplateSaveDto sendByTemplateRequestDto);
    }
}