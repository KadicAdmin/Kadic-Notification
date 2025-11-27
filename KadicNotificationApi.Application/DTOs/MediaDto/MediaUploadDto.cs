namespace KadicNotificationApi.Application.DTOs.MediaDto;
public class MediaUploadDto
{
    public string Title { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public byte[] FileData { get; set; } = null!;
}