namespace GlossaryEng.Auth.Models.EmailConfiguration;

public class EmailConfiguration
{
    public string EmailSender { get; set; }
    public string NameSender { get; set; }
    public string Password { get; set; }
    public string SmtpServer { get; set; }
    public int Port { get; set; }
    public bool UseSsl { get; set; }
}