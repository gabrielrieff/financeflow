using AutoMapper;
using DocumentFormat.OpenXml.Office2016.Excel;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Repositories.Recurrences;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Accounts.Update;

public class UpdateAccountUseCase : IUpdateAccountUseCase
{
    private readonly IAccountUpdateOnlyRepository _repositoryAccountUpdate;
    private readonly IRecurrenceUpdateOnlyRepository _repositoryRecurrenceUpdate;
    private readonly IRecurrenceWhiteOnlyRepository _repositoryReccurenceWhite;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public UpdateAccountUseCase(
        IAccountUpdateOnlyRepository repositoryAccountUpdate,
        IRecurrenceUpdateOnlyRepository repositoryRecurrenceUpdate,
        IRecurrenceWhiteOnlyRepository repositoryReccurenceWhite,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILoggedUser loggedUser)
    {
        _repositoryAccountUpdate = repositoryAccountUpdate;
        _repositoryRecurrenceUpdate = repositoryRecurrenceUpdate;
        _repositoryReccurenceWhite = repositoryReccurenceWhite;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;

    }

    public async Task Execute(long id, AccountRequestJson request)
    {
        ValidatorAccount(request);
        var loggedUser = await _loggedUser.Get();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var account = await _repositoryAccountUpdate.GetById(loggedUser, id);

            if (account is null)
            {
                throw new NotFoundException("Conta não existe.");
            }

            account.Tags.Clear();

            _mapper.Map(request, account);

            account.Update_at = DateTime.UtcNow;

             _repositoryAccountUpdate.Update(account);
             await _unitOfWork.Commit();

            // Recorrência
            if (request.Start_Date != request.End_Date)
            {
                var recurrence = await _repositoryRecurrenceUpdate.GetByIdAccount(account.ID);

                if(recurrence is not null)
                {
                    UpdateRecurrence(account.ID, request);
                }
                else
                {
                    CreateRecurrence(account.ID, request);
                }
            }

            // Confirmação final da transação
            await _unitOfWork.CommitTransactionAsync();
        }
        catch (NotFoundException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new NotFoundException("Erro ao realizar o registro");
        }

    }

    private async void UpdateRecurrence(long accountId, AccountRequestJson request)
    {
        var recurrence = await _repositoryRecurrenceUpdate.GetByIdAccount(accountId);
        _mapper.Map(request, recurrence);
        recurrence!.Update_at = DateTime.UtcNow;

        _repositoryRecurrenceUpdate.Update(recurrence);
    }
    
    private async void CreateRecurrence(long accountId, AccountRequestJson request)
    {
        var reccurence = _mapper.Map<Recurrence>(request);
        reccurence.AccountID = accountId;
        reccurence.Create_at = DateTime.UtcNow;
        reccurence.Update_at = DateTime.UtcNow;

        await _repositoryReccurenceWhite.Add(reccurence);

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
}
