namespace KadicNotificationApi.Application.DTOs
{
    public class SendByTemplateRequestDto
    {
        public int TemplateId { get; set; }
        public string[] To { get; set; } = [];        
    }
}