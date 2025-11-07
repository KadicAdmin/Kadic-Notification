using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Domain.Entities
{
    public class EmailTemplateType
    {
        public int Id { get; set; }
        public string HtmlBody { get; set; } 
    }
}
