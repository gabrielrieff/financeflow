using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Services.CodeHash;
using FinanceFlow.Domain.Services.SendMail;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Users.RecoverPassword;

public class RecoverPasswordUseCase : IRecoverPasswordUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserUpdateOnlyRepository _userUpdateRepository;
    private readonly IPasswordEncripter _passwordEncriter;
    private readonly IEmailService _sendMail;
    private readonly ICodeHash _code;
    private readonly IUnitOfWork _unitOfWork;


    public RecoverPasswordUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserUpdateOnlyRepository userUpdateRepository,
        IPasswordEncripter passwordEncriter,
        IEmailService sendMail,
        ICodeHash code,
        IUnitOfWork unitOfWork
        )
    {
        _passwordEncriter = passwordEncriter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userUpdateRepository = userUpdateRepository;
        _sendMail = sendMail;
        _code = code;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestRecoverPasswordJson request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if(user is null)
        {
            throw new NotFoundException("User not found!");
        }

        var code = _code.CreateCode();

        user.CodePassword = code;

        _userUpdateRepository.Update(user);

        var template = Path.Combine(Directory.GetCurrentDirectory(), "../FinanceFlow.Infrastructure/TemplateEmail/RecoverPassword.html");
        var htmlBody = File.ReadAllText(template);

        htmlBody = htmlBody.Replace("{{name}}", user.Name);
        htmlBody = htmlBody.Replace("{{code}}", code);


        await _sendMail.SendMail(
            emailsTo: [user.Email],
            subject: "teste",
            RouterTemplate: htmlBody
            );

        await _unitOfWork.Commit();
    }
}
