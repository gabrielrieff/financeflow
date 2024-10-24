﻿using AutoMapper;
using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Repositories.Transactions;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Accounts.Register;

public class RegisterAccountUseCase : IRegisterAccountUseCase
{

    private readonly IAccountWhiteOnlyRepository _repository;
    private readonly IRecurrenceWhiteOnlyRepository _repositoryReccurence;
    private readonly ITransactionWhiteOnlyRepository _repositoryTransction;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILoggedUser _loggedUser;

    public RegisterAccountUseCase(
                IAccountWhiteOnlyRepository repository,
                IRecurrenceWhiteOnlyRepository repositoryReccurence,
                ITransactionWhiteOnlyRepository repositoryTransction,
                IUnitOfWork unitOfWork,
                IMapper mapper,
                ILoggedUser loggedUser)
    {
        _repository = repository;
        _repositoryReccurence = repositoryReccurence;
        _repositoryTransction = repositoryTransction;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _loggedUser = loggedUser;
    }

    public async Task<string> Execute(AccountRequestJson request)
    {
        ValidatorAccount(request);
        var loggedUser = await _loggedUser.Get();

        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Criação de conta
            var accountMapper = _mapper.Map<Account>(request);
            accountMapper.UserID = loggedUser.Id;
            accountMapper.Status = true;
            accountMapper.Create_at = DateTime.UtcNow;
            accountMapper.Update_at = DateTime.UtcNow;


            var account = await _repository.Add(accountMapper);
            await _unitOfWork.Commit();

            // Recorrência
            if (request.Start_Date != request.End_Date)
            {
                var reccurence = _mapper.Map<Recurrence>(request);
                reccurence.AccountID = account.ID;
                reccurence.Create_at = DateTime.UtcNow;
                reccurence.Update_at = DateTime.UtcNow;

                await _repositoryReccurence.Add(reccurence);
            }

            // Confirmação final da transação
            await _unitOfWork.CommitTransactionAsync();

            return "Registro concluído";
        }
        catch (NotFoundException ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new NotFoundException("Erro ao realizar o registro");
        }
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