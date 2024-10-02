using commonTestUtilities.Cryptography;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.User;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Requests.User;
using commonTestUtilities.Token;
using FinanceFlow.Application.UseCases.Users.Register;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Users.Register;

public class RegisterUserUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase();
        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.NAME_REQUIRED));
    }

    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestRegisterUserJsonBuilder.Build();
        var useCase = CreateUseCase(request.Email);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.EMAIL_EXIST));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var userWhiteOnly = UserWhiteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var passwordEncripter = new PasswordEncripterBuilder().Build();
        var initOfWork = UnitOfWorkBuilder.Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            userReadOnlyRepository.ExistActiveUserWithEmail(email);
        }

        return new RegisterUserUseCase(mapper, passwordEncripter, userReadOnlyRepository.Build(), userWhiteOnly, initOfWork, jwtTokenGenerator);
    }
}
