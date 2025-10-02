using KadicNotificationApi.Application.DTOs;


namespace KadicNotificationApi.Application.Interfaces
{
    public interface IEmailSender
    {
        Task SendAsync(SendEmailRequest request, CancellationToken cancellationToken = default);     
    }
}