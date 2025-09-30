using KadicNotificationApi.Application.DTOs;
using KadicNotificationApi.Application.Interfaces;
using KadicNotificationApi.Infraestructure.Config;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;



namespace KadicNotificationApi.Application.Services;

public class SendEmailService : IEmailSender
{
    private readonly EmailSettings _opt;

    public SendEmailService(IOptions<EmailSettings> opt)
    {
        _opt = opt.Value;
    }

    public async Task SendAsync(SendEmailRequest req, CancellationToken ct = default)
    {
        try
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(_opt.FromName, _opt.FromAddress));

            void AddMany(InternetAddressList list, List<EmailAddressDto>? addrs)
            {
                if (addrs == null) return;
                foreach (var a in addrs)
                    list.Add(new MailboxAddress(a.Name ?? string.Empty, a.Address));
            }

            AddMany(message.To, req.To);
            AddMany(message.Cc, req.Cc);
            AddMany(message.Bcc, req.Bcc);

            message.Subject = req.Subject;

            var bodyBuilder = new BodyBuilder();

            if (!string.IsNullOrWhiteSpace(req.HtmlBody))
                bodyBuilder.HtmlBody = req.HtmlBody;

            if (!string.IsNullOrWhiteSpace(req.TextBody))
                bodyBuilder.TextBody = req.TextBody;

            if (req.Attachments != null)
            {
                foreach (var a in req.Attachments)
                {
                    var bytes = Convert.FromBase64String(a.ContentBase64);
                    bodyBuilder.Attachments.Add(a.FileName, bytes, ContentType.Parse(a.ContentType ?? "application/octet-stream"));
                }
            }

            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient(); // Ensure MailKit's SmtpClient is used

            client.Timeout = _opt.TimeoutSeconds * 1000;

            var secure = _opt.Security?.ToLowerInvariant() switch
            {
                "sslonconnect" => SecureSocketOptions.SslOnConnect,
                "starttls" => SecureSocketOptions.StartTls,
                "none" => SecureSocketOptions.None,
                _ => SecureSocketOptions.StartTls
            };

            await client.ConnectAsync(_opt.SmtpHost, _opt.SmtpPort, secure, ct);

            // Algunos servidores requieren quitar OAuth mecánicas
            //client.AuthenticationMechanisms.Remove("XOAUTH2");

            await client.AuthenticateAsync(_opt.Username, _opt.Password, ct);
            await client.SendAsync(message, ct);
            await client.DisconnectAsync(true, ct);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}






