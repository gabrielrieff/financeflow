namespace FinanceFlow.Domain.Services.SendMail;
public interface IEmailService
{
    Task SendMail(List<string> emailsTo, string subject, string RouterTemplate, bool isHtml = true);
}
