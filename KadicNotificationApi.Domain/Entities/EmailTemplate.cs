using System.ComponentModel.DataAnnotations.Schema;

namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplate
    {
        public int Id { get; set; }
        public string Subjet { get; set; }       
        public int EmailTemplatesTypeId { get; set; }
        [ForeignKey("EmailTemplatesTypeId")]
        public EmailTemplatesType Type { get; set; }
        public string Description { get; set; }        
    }
}