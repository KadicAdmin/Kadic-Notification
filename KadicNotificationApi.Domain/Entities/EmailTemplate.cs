namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplate
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }   // Nombre identificador de la plantilla
        public string Subject { get; private set; }
        public string Body { get; private set; }

        public EmailTemplate(string name, string subject, string body)
        {
            Id = Guid.NewGuid();
            Name = name;
            Subject = subject;
            Body = body;
        }

        public string ApplyPlaceholders(Dictionary<string, string> values)
        {
            string result = Body;
            foreach (var kv in values)
                result = result.Replace($"{{{kv.Key}}}", kv.Value);

            return result;
        }
    }
}