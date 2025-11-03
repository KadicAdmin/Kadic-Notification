namespace KadicNotificationApi.Application.DTOs
{
    public class EmailTemplateDto
    {
        public int Id { get; set; }

        public string Subject { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int EmailTemplatesTypeId { get; set; }
    }
}