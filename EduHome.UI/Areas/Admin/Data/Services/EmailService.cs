using EduHome.UI.Helpers;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace EduHome.UI.Areas.Admin.Data.Services;

public class EmailService : IEmailService
{

    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings=emailSettings.Value;
    }
    public void Send(string to, string subject, string html, string form = null)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(form ?? _emailSettings.FormAddres));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        using var smtp = new SmtpClient();
        smtp.Connect(_emailSettings.Server, _emailSettings.Port,SecureSocketOptions.StartTls);
        smtp.Authenticate(_emailSettings.UserName,_emailSettings.Password);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
