namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplate
    {       
        public int Id { get; set; }
        public string Subject { get; set; }
        public int TemplateTypeId { get; set; }
        public EmailTemplateType? TemplateType { get; set; }
        public int  TenantId { get; set; }
    }
}