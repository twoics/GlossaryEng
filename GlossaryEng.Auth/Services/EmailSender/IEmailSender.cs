namespace GlossaryEng.Auth.Services.EmailSender;

public interface IEmailSender
{
    Task SendEmailAsync(string recipientEmail, string subject, string message);
}