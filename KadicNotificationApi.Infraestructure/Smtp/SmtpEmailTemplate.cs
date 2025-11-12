using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.Config;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace KadicNotificationApi.Infraestructure.SMTP;

public class SmtpEmailTemplate
{
    private readonly EmailSettings _options;

    public SmtpEmailTemplate(IOptions<EmailSettings> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(EmailTemplate template, IEnumerable<string> to)
    {
        using var client = new SmtpClient(_options.SmtpServer, _options.Port)
        {
            Credentials = new NetworkCredential(_options.Username, _options.Password),
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            Timeout = _options.TimeoutSeconds * 1000
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(_options.FromAddress, _options.FromName),
            Subject = template.Subject?.Trim() ?? string.Empty,
            Body = (template.TemplateType?.HtmlBody ?? string.Empty).Trim(),
            IsBodyHtml = true,

            SubjectEncoding = Encoding.UTF8,
            BodyEncoding = Encoding.UTF8
        };

        foreach (var addr in to) mail.To.Add(addr.Trim());

        if (mail.To.Count == 0)
            throw new InvalidOperationException("Debe especificar al menos un destinatario.");

        await client.SendMailAsync(mail);
    }
}