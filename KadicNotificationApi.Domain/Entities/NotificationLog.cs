using KadicNotificationApi.Domain.Enums;

namespace KadicNotificationApi.Domain.Entities
{
    public class NotificationLog
    {
        public Guid Id { get; private set; }
        public EmailAddress To { get; private set; }
        public string Subject { get; private set; }
        public DateTime SentAt { get; private set; }
        public EmailStatus Status { get; private set; }
        public string? ErrorMessage { get; private set; }

        private NotificationLog() { }

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