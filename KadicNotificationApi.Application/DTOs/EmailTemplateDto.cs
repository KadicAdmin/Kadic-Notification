namespace KadicNotificationApi.Application.DTOs
{
    public class EmailTemplateDto
    {        
        public string Subject { get; set; } = string.Empty;
        public string? TextBody { get; set; }
        public int TemplateTypeId { get; set; }
    }
}