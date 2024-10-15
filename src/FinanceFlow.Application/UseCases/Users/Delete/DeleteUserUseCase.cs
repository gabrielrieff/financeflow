
using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Services.LoggedUser;

namespace FinanceFlow.Application.UseCases.Users.DeleteUser;

public class DeleteUserUseCase : IDeleteUserUseCase
{
    private readonly IUserWhiteOnlyRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILoggedUser _loggedUser;


    public DeleteUserUseCase(IUserWhiteOnlyRepository repository,
                            IUnitOfWork unitOfWork,
                           ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute()
    {
        var user = await _loggedUser.Get();

        await _repository.Delete(user);

        await _unitOfWork.Commit();

    }
}
