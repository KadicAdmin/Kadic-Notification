using KadicNotificationApi.Infraestructure.Config;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using KadicNotificationApi.Domain.Entities;

namespace KadicNotificationApi.Infraestructure.Smtp;

public class SmtpEmailSender 
{
    private readonly EmailSettings _options;

    public SmtpEmailSender(IOptions<EmailSettings> options)
    {
        _options = options.Value;
    }

    public async Task SendEmailAsync(EmailMessage request)
    {
        using var client  =  new SmtpClient(_options.SmtpServer, _options.Port)
        {
            Credentials = new NetworkCredential(_options.Username, _options.Password),
            EnableSsl = true
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_options.From),
            Subject = request.Subject,
            Body = request.Body,
            IsBodyHtml = request.IsBodyHtml
        };

        foreach (var to in request.To)
            mail.To.Add(new MailAddress(to.Address, to.Name));

        if (request.Cc != null)
        {
            foreach (var cc in request.Cc)
                mail.CC.Add(new MailAddress(cc.Address, cc.Name));
        }

        if (request.Bcc != null)
        {
            foreach (var bcc in request.Bcc)
                mail.Bcc.Add(new MailAddress(bcc.Address, bcc.Name));
        }
    }     
}