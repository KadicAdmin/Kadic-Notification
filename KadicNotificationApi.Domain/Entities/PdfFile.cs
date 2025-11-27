namespace KadicNotificationApi.Domain.Entities;
public class PdfFile
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string FileName { get; set; } = null!;
    public string ContentType { get; set; } = "application/pdf";
    public byte[] FileData { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}