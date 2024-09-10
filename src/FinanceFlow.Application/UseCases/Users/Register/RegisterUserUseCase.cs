using AutoMapper;
using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Communication.Responses.Users;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FinanceFlow.Infrastructure.DataAccess;
using FluentValidation.Results;

namespace FinanceFlow.Application.UseCases.Users.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{

    private readonly IMapper _mapper;
    private readonly IPassawordEncripter _passwordEncriter;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IUserWhiteOnlyRepository _userWhiteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;


    public RegisterUserUseCase(
        IMapper mapper,
        IPassawordEncripter passwordEncriter,
        IUserReadOnlyRepository userReadOnlyRepository,
        IUserWhiteOnlyRepository userWhiteOnlyRepository,
        IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _passwordEncriter = passwordEncriter;
        _userReadOnlyRepository = userReadOnlyRepository;
        _userWhiteOnlyRepository = userWhiteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncriter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWhiteOnlyRepository.Add(user);

        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
        };
    }

    private async Task Validate(RequestUserJson request)
    {
        var result = new RegisterUserValidator().Validate(request);

        var emailExist = await _userReadOnlyRepository.ExistActiveUserWithEmail(request.Email);

        if (emailExist)
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorsMessage.EMAIL_EXIST));
        }

        if (result.IsValid == false) 
        { 
            var errorMessagens  = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessagens);
        }
    }
}
