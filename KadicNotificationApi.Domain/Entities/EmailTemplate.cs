namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; set; }
        public string Subject { get; set; } = default!;
        public string? HtmlBody { get; set; }
        
        public EmailTemplate( string subject, string htmlBody)
        {
            Id = Guid.NewGuid();
            Subject = subject;
            HtmlBody = htmlBody;
            
        }     
    }
}