using commonTestUtilities.Cryptography;
using commonTestUtilities.Entities;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Requests.User;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Users.UpdatePassword;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Users.ChangePassword;

public class ChangePasswordUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user,request.Password);
        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async Task Error_NewPassword_Empty()
    {
        var user = UserBuilder.Build();

        var request = RequestChangePasswordJsonBuilder.Build();
        request.NewPassword = string.Empty;

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => { await useCase.Execute(request); };

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(e => e.GetErrors().Count == 1 &&
                e.GetErrors().Contains(ResourceErrorsMessage.INVALID_PASSWORD));
    }

    [Fact]
    public async Task Error_CurrentPassword_Different()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }

    private ChangePasswordUseCase CreateUseCase(User user, string? password = null)
    {
        var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
        var initOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();

        return new ChangePasswordUseCase(
            updateRepository: userUpdateOnlyRepository,
            initOfWork,
            loggedUser,
            Encripter: passwordEncripter);
    }
}
