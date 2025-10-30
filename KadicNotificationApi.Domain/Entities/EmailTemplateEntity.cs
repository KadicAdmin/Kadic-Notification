namespace KadicNotificationApi.Infraestructure.Persistence.Repositories
{
    public class EmailTemplateEntity
    {
        public int Id { get; set; }

        public string Subject { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int EmailTemplatesTypeId { get; set; }
    }
}