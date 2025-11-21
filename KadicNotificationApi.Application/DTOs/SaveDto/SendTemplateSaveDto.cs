namespace KadicNotificationApi.Application.DTOs.SaveDto
{
    public class SendTemplateSaveDto
    {
        public int TemplateId { get; set; }
        public string[] To { get; set; } = [];
    }
}