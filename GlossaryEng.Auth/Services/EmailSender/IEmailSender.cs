using GlossaryEng.Auth.Models.CustomResult;

namespace GlossaryEng.Auth.Services.EmailSender;

public interface IEmailSender
{
    /// <summary>
    /// Sends an email to the specified email address
    /// </summary>
    /// <param name="recipientEmail">Which mail to send the letter to</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="message">Letter text, supports HTML </param>
    /// <returns>CustomResult object that contains the method execution status and error, if any</returns>
    Task<CustomResult> SendEmailAsync(string recipientEmail, string subject, string message);
}