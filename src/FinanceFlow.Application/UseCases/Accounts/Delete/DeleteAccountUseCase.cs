using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Exception.ExceptionBase;

namespace FinanceFlow.Application.UseCases.Accounts.Delete;

public class DeleteAccountUseCase  : IDeleteAccountUseCase
{
    private readonly IAccountWhiteOnlyRepository _repositoryWhite;
    private readonly IAccountsReadOnlyRepository _repositoryRead;
    private readonly ILoggedUser _loggedUser;
    private readonly IUnitOfWork _unitOfWork;


    public DeleteAccountUseCase(IAccountsReadOnlyRepository repositoryRead,
        IAccountWhiteOnlyRepository repositoryWhite,
        ILoggedUser loggedUser,
        IUnitOfWork unitOfWork)
    {
        _repositoryRead = repositoryRead;
        _repositoryWhite = repositoryWhite;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(long id)
    {
        var loggedUser = await _loggedUser.Get();

        var account = await _repositoryRead.GetById(loggedUser, id);

        if(account is null)
        {
            throw new NotFoundException("Conta não encontrada");
        }

        await _repositoryWhite.DeleteById(id);

        await _unitOfWork.Commit();

    }
}
