using KadicNotificationApi.Domain.Entities;
using KadicNotificationApi.Infraestructure.Config;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;



namespace KadicNotificationApi.Infraestructure.MailKit;

public class MailKitEmailSenderTest 
{
    private readonly EmailSettingsTest _options;

    public MailKitEmailSenderTest(IOptions<EmailSettingsTest> options)
    {
        _options = options.Value;
    }

public async Task SendAsync(EmailMessage request)
{
var message = new MimeMessage();
message.From.Add(new MailboxAddress(_options.DisplayName, _options.From));


foreach (var to in request.To)
    message.To.Add(new MailboxAddress(to.Name, to.Address));

if (request.Cc != null)
{
    foreach (var cc in request.Cc)
        message.Cc.Add(new MailboxAddress(cc.Name, cc.Address));
}

if (request.Bcc != null)
{
    foreach (var bcc in request.Bcc)
        message.Bcc.Add(new MailboxAddress(bcc.Name, bcc.Address));
}

message.Subject = request.Subject;

message.Body = new TextPart(request.IsBodyHtml ? "html" : "plain")
{
    Text = request.Body
};

using var client = new SmtpClient();
await client.ConnectAsync(_options.SmtpServer, _options.Port, true);
await client.AuthenticateAsync(_options.Username, _options.Password);
await client.SendAsync(message);
await client.DisconnectAsync(true);
}
    
}