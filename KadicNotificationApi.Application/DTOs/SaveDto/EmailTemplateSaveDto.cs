namespace KadicNotificationApi.Application.DTOs.SaveDto
{
    public class EmailTemplateSaveDto
    {
        public string Subject { get; set; }
        public int TemplateTypeId { get; set; }
        public int TenantId { get; set; }
    }
}