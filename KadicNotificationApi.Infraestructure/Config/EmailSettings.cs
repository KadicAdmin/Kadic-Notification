namespace KadicNotificationApi.Infraestructure.Config;

public class EmailSettings
{
    public string SmtpHost { get; set; } = default!;
    public int SmtpPort { get; set; }
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string FromName { get; set; } = default!;
    public string FromAddress { get; set; } = default!;
    // None | SslOnConnect | StartTls
    public string Security { get; set; } = "StartTls";
    public int TimeoutSeconds { get; set; } = 30;
    public string DisplayName { get; set; } = string.Empty;
    public string From { get; set; } = string.Empty;
    public int Port { get; set; }
    public string SmtpServer { get; set; } = string.Empty;
}