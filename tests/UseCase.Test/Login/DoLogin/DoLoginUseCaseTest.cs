using commonTestUtilities.Cryptography;
using commonTestUtilities.Entities;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Requests.User;
using commonTestUtilities.Token;
using FinanceFlow.Application.UseCases.Login;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Login.DoLogin;

public class DoLoginUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user, request.Password);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_User_Not_Found()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();

        var useCase = CreateUseCase(user, request.Password);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginExpection>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.EMAIL_OR_PASSWORD_INVALID));
    }

    [Fact]
    public async Task Error_Password_Not_Match()
    {
        var user = UserBuilder.Build();
        var request = RequestLoginJsonBuilder.Build();
        request.Email = user.Email;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<InvalidLoginExpection>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.EMAIL_OR_PASSWORD_INVALID));
    }

    private DoLoginUseCase CreateUseCase(User user, string? password = null)
    {
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().GetUserByEmail(user).Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();
        var passwordEncripter = new PasswordEncripterBuilder().Verify(password).Build();

        return new DoLoginUseCase(userReadOnlyRepository, passwordEncripter, jwtTokenGenerator);
    }
}
