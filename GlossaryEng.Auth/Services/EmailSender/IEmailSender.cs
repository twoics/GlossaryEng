using GlossaryEng.Auth.Models.CustomResult;

namespace GlossaryEng.Auth.Services.EmailSender;

public interface IEmailSender
{
    Task<CustomResult> SendEmailAsync(string recipientEmail, string subject, string message);
}