using GlossaryEng.Auth.Models.CustomResult;
using GlossaryEng.Auth.Models.EmailConfiguration;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace GlossaryEng.Auth.Services.EmailSender;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _emailConfiguration;

    public EmailSender(EmailConfiguration emailConfiguration)
    {
        _emailConfiguration = emailConfiguration;
    }

    public async Task<CustomResult> SendEmailAsync(string recipientEmail, string subject, string message)
    {
        var mimeMessage = new MimeMessage();

        mimeMessage.From.Add(new MailboxAddress(_emailConfiguration.NameSender, _emailConfiguration.EmailSender));
        mimeMessage.To.Add(new MailboxAddress(string.Empty, recipientEmail));
        mimeMessage.Subject = subject;
        mimeMessage.Body = new TextPart(TextFormat.Plain) { Text = message };

        using var client = new SmtpClient();
        try
        {
            await client.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.Port,
                _emailConfiguration.UseSsl);

            await client.AuthenticateAsync(_emailConfiguration.EmailSender, _emailConfiguration.Password);

            await client.SendAsync(mimeMessage);
            await client.DisconnectAsync(true);
        }
        catch
        {
            return new CustomResult(false, "Can't send message");
        }

        return new CustomResult(true);
    }
}