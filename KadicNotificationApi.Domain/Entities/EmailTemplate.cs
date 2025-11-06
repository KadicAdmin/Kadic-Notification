namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplate
    {       
        public int Id { get; set; }
        public string Subject { get; set; } 
        public string? TextBody { get; set; }
        public int TemplateTypeId { get; set; }
    }
}
