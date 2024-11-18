using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Services.CodeHash;
using FinanceFlow.Domain.Services.SendMail;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentValidation.Results;

namespace FinanceFlow.Application.UseCases.Users.RecoverPasswordWithCode;

public class RecoverPasswordWithCodeUseCase : IRecoverPasswordWithCodeUseCase
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserUpdateOnlyRepository _userUpdateRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IEmailService _sendMail;
    private readonly ICodeHash _code;
    private readonly IUnitOfWork _unitOfWork;


    public RecoverPasswordWithCodeUseCase(
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserUpdateOnlyRepository userUpdateRepository,
        IPasswordEncripter passwordEncripter,
        IEmailService sendMail,
        ICodeHash code,
        IUnitOfWork unitOfWork
        )
    {
        _passwordEncripter = passwordEncripter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userUpdateRepository = userUpdateRepository;
        _sendMail = sendMail;
        _code = code;
        _unitOfWork = unitOfWork;
    }


    public async Task Execute(RequestRecoverPasswordWithCode request)
    {
        var user = await _userReadOnlyRepository.GetUserByEmail(request.Email);

        if (user is null)
        {
            throw new NotFoundException("User not found!");
        }

        var alredyCode = await _code.VerifyCode(request.Code, request.Email);

        if (!alredyCode)
        {
            throw new NotFoundException("Code invalid!");
        }

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);
        user.CodePassword = "";

        _userUpdateRepository.Update(user);

        await _unitOfWork.Commit(); 

    }
}
