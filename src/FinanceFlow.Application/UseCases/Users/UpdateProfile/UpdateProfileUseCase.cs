using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentValidation.Results;

namespace FinanceFlow.Application.UseCases.Users.UpdateProfile;

public class UpdateProfileUseCase : IUpdateProfileUseCase
{
    private readonly IUserReadOnlyRepository _readOnlyRepository;
    private readonly IUserUpdateOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;


    public UpdateProfileUseCase(
        IUserReadOnlyRepository readOnlyRepository, 
        IUserUpdateOnlyRepository repository, 
        IUnitOfWork unitOfWork,
        ILoggedUser loggedUser)
    {
        _readOnlyRepository = readOnlyRepository;
        _repository = repository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateProfileJson request)
    {
        var loggedUser = await _loggedUser.Get();

        await Validate(request, loggedUser.Email);

        var user = await _repository.GetById(loggedUser.Id);

        user.Name = request.Name;
        user.Email = request.Email;

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateProfileJson request, string currentEmail)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        if(currentEmail.Equals(request.Email) == false)
        {
            var userExist = await _readOnlyRepository.ExistActiveUserWithEmail(request.Email);
            if (userExist)
            {
                result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorsMessage.EMAIL_ALREADY_REGISTERED));
            }
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
