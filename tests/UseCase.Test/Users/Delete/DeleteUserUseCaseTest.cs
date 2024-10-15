using commonTestUtilities.Entities;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Users.DeleteUser;
using FinanceFlow.Domain.Entities;
using FluentAssertions;

namespace UseCase.Test.Users.Delete;

public class DeleteUserUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute();

        await act.Should().NotThrowAsync();
    }


    private DeleteUserUseCase CreateUseCase(User user)
    {
        var repository = UserWhiteOnlyRepositoryBuilder.Build();
        var initOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new DeleteUserUseCase(repository, initOfWork, loggedUser);
    }
}
