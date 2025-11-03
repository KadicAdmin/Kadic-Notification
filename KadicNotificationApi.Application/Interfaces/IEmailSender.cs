using KadicNotificationApi.Application.DTOs.GetDto;


namespace KadicNotificationApi.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(SendEmailRequest request);     
    }
}