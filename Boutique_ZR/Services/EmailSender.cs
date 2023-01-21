
using Boutique_ZR.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace Boutique_ZR.Services;


public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly BoutiqueDbContext _boutiqueDbContext;

    public EmailSender(IConfiguration configuration,BoutiqueDbContext boutiqueDbContext)
    {
        _configuration = configuration; 
        _boutiqueDbContext = boutiqueDbContext;
    }
    public void Send(string[] allTo, string subject, string html)
    {   
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_configuration["Mail:UserMail"]));
        foreach (var to in allTo)
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_configuration.GetValue<string>("Mail:Host"), _configuration.GetValue<int>("Mail:Port"), SecureSocketOptions.StartTls);
        smtp.Authenticate(_configuration.GetValue<string>("Mail:UserMail"), _configuration.GetValue<string>("Mail:Password"));
        smtp.Send(email);
        smtp.Disconnect(true);
    }
    public void Send(string to, string subject, string html)
    {
        Send(new[] { to },subject,html);
    }

}