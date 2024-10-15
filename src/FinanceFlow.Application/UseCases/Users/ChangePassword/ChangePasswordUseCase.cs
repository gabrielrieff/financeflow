using FinanceFlow.Application.UseCases.Users.UpdateProfile;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentValidation.Results;

namespace FinanceFlow.Application.UseCases.Users.UpdatePassword;

public class ChangePasswordUseCase : IChangePasswordUseCase
{
    private readonly IUserUpdateOnlyRepository _updateRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;
    private readonly IPasswordEncripter _passwordEncripter;


    public ChangePasswordUseCase(
        IUserUpdateOnlyRepository updateRepository, 
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser,
        IPasswordEncripter Encripter)
    {
        _updateRepository = updateRepository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
        _passwordEncripter = Encripter;
    }

    public async Task Execute(RequestChangePasswordJson request)
    {
        var loggedUser = await _loggedUser.Get();

        Validate(request, loggedUser);

        var user = await _updateRepository.GetById(loggedUser.Id);

        user.Password = _passwordEncripter.Encrypt(request.NewPassword);

        _updateRepository.Update(user);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestChangePasswordJson request, User loggedUser)
    {
        var validator = new ChangePasswordValidator();

        var result = validator.Validate(request);

        var passwordMatch = _passwordEncripter.Verify(request.Password, loggedUser.Password);

        if (passwordMatch == false)
        {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorsMessage.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
