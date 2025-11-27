using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadicNotificationApi.Application.DTOs.MediaDto
{
    public class MediaDownloadDto
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public byte[] FileData { get; set; } = null!;
    }
}
