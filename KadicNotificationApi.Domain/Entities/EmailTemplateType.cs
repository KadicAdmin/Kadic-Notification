namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplateType
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = default!;
    }
}
