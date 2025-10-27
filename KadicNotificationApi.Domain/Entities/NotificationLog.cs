using KadicNotificationApi.Domain.Enums;
using KadicNotificationApi.Domain.ValueObjects;

namespace KadicNotificationApi.Domain.Entities
{
    public class NotificationLog
    {
        public Guid Id { get; set; }
        public EmailAddress To { get; set; }
        public string Subject { get; set; }
        public DateTime SentAt { get; set; }
        public EmailStatus Status { get; set; }
        public string? ErrorMessage { get; set; }
        

        public NotificationLog(EmailAddress to, string subject, EmailStatus status, string? errorMessage = null)
        {
            Id = Guid.NewGuid();
            To = to;
            Subject = subject;
            SentAt = DateTime.UtcNow;
            Status = status;
            ErrorMessage = errorMessage;
        }
    }
}