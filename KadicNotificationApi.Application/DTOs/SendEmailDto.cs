namespace KadicNotificationApi.Application.DTOs;

public class EmailAddressDto
{
    public string Address { get; set; }
    public string? Name { get; set; }
}
public class AttachmentDto
{
    public string FileName { get; set; }
    public string ContentBase64 { get; set; }
    public string? ContentType { get; set; }
}
public class SendEmailRequest
{
    public List<EmailAddressDto> To { get; set; } = new();
    public List<EmailAddressDto>? Cc { get; set; }
    public List<EmailAddressDto>? Bcc { get; set; }

    public string Subject { get; set; }
    public string? HtmlBody { get; set; }
    public string? TextBody { get; set; }

    public List<AttachmentDto>? Attachments { get; set; }
}