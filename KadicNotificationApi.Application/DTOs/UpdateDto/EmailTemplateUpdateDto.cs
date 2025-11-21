namespace KadicNotificationApi.Application.DTOs.UpdateDto
{
    public class EmailTemplateUpdateDto
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public int TemplateTypeId { get; set; }
        public int TenantId { get; set; }
    }
}