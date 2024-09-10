using FinanceFlow.Exception.Resource;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace FinanceFlow.Application.UseCases.Users;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{

    private const string Error_message_key = "ErrorMessagem";
    public override string Name => "PasswordValidator";

    protected override string GetDefaultMessageTemplate(string errorCode)
    {
        return $"{{{Error_message_key}}}";
    }

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            context.MessageFormatter.AppendArgument(Error_message_key, ResourceErrorsMessage.INVALID_PASSWORD);
            return false;
        }

        if(password.Length < 8)
        {
            context.MessageFormatter.AppendArgument(Error_message_key, ResourceErrorsMessage.INVALID_PASSWORD);
            return false;
        }
        
        if(RegexPassword().IsMatch(password) == false)
        {
            context.MessageFormatter.AppendArgument(Error_message_key, ResourceErrorsMessage.INVALID_PASSWORD);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[A-Za-z0-9\\!\\?\\*\\.]")]
    private static partial Regex RegexPassword();
}
