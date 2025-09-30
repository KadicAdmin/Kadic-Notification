namespace KadicNotificationApi.Domain.Entities;

public record EmailAddress(string Address, string? Name);

public record Attachment(string FileName, string ContentBase64, string? ContentType);

public class EmailMessage
{
    public List<EmailAddress> To { get; set; } = new();
    public List<EmailAddress>? Cc { get; set; }
    public List<EmailAddress>? Bcc { get; set; }

    public string Subject { get; set; } = string.Empty;

    public string? HtmlBody { get; set; }
    public string? TextBody { get; set; } 

    public List<Attachment>? Attachments { get; set; }

    /// <summary>
    /// Devuelve true si el cuerpo principal está en HTML.
    /// </summary>
    public bool IsBodyHtml => !string.IsNullOrWhiteSpace(HtmlBody);

    /// <summary>
    /// Devuelve el cuerpo principal (HtmlBody si existe, en su defecto TextBody).
    /// </summary>
    public string? Body => HtmlBody ?? TextBody;
}