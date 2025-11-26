namespace KadicNotificationApi.Application.DTOs.GetDto
{
    public class EmailTemplateGetDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int TemplateTypeId { get; set; }
        public int TenantId { get; set; }
    }
}