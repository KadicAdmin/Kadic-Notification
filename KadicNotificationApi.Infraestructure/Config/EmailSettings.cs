namespace KadicNotificationApi.Infraestructure.Config
{
    public class EmailSettings
    { 
        public string Username { get; set; } 
        public string Password { get; set; } 
        public string FromName { get; set; }   
        public string Security { get; set; } = "StartTls";
        public int TimeoutSeconds { get; set; } = 30;
        public string FromAddress { get; set; } 
        public string SmtpServer { get; set; }
        public int Port { get; set; }  
        
    }
}