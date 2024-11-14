using FinanceFlow.Domain.Services.SendMail;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FinanceFlow.Infrastructure.Services.SendMail;

public class EmailService : IEmailService
{
    private readonly SmtpClient _smtpClient;
    public EmailService(SmtpClient smtpClient)
    {
        _smtpClient = smtpClient;
    }

    public async Task SendMail(List<string> emailsTo, string subject, string RouterTemplate, bool isHtml)
    {
        var mail = PrepareMessage(emailsTo, subject, RouterTemplate, isHtml);

        await _smtpClient.SendMailAsync(mail);
    }

    private MailMessage PrepareMessage(List<string> emailsTo, string subject, string RouterTemplate, bool isHtml)
    {
        var mail = new MailMessage
        {
            From = new MailAddress("gabeerieff@gmail.com", "Equipe finance flow"), // Altere para um endereço válido
            Subject = subject,
            Body = RouterTemplate,
            IsBodyHtml = isHtml
        };

        foreach (var email in emailsTo)
        {
            if (ValidateEmail(email))
            {
                mail.To.Add(email);
            }
        }

        return mail;
    }

    private bool ValidateEmail(string email)
    {
        Regex expression = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
        if (expression.IsMatch(email))
            return true;

        return false;
    }
}
