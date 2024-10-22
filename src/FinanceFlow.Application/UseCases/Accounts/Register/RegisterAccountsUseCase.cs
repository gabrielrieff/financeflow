using AutoMapper;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Accounts.Register;

public class RegisterAccountUseCase : IRegisterAccountUseCase
{

    private readonly IAccountWhiteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterAccountUseCase(
                IAccountWhiteOnlyRepository repository,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ILoggedUser loggedUser)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<string> Execute(AccountRequestJson request)
    {
        ValidatorAccount(request);
        var loggedUser = await _loggedUser.Get();

        var accountMapper = _mapper.Map<Account>(request);
        accountMapper.UserID = loggedUser.Id;

        var account = await _repository.Add(accountMapper);

        await _unitOfWork.Commit();

        if (request.RecurrenceRequestJson is not null)
        {
            //validar e salvar uma recorrencia
            ValidatorRecurrence(request.RecurrenceRequestJson);
            var reccurence = _mapper.Map<Recurrence>(request.RecurrenceRequestJson);
            reccurence.AccountID = account.ID;

            await _unitOfWork.Commit();
        }

        if (request.TransactionRequestJson is not null)
        {
            //validar e salvar uma transação

            await _unitOfWork.Commit();
        }

        return "d";
    }

    private void ValidatorAccount(AccountRequestJson entiti)
    {
        var validator = new AccountRegisterValidator();

        var result = validator.Validate(entiti);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
    
    private void ValidatorRecurrence(RecurrenceRequestJson entiti)
    {
        var validator = new ReccurenceRegisterValidator();

        var result = validator.Validate(entiti);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}