using commonTestUtilities.Cryptography;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Requests;
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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var userWhiteOnly = UserWhiteOnlyRepositoryBuilder.Build();
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder().Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var initOfWork = UnitOfWorkBuilder.Build();
        var jwtTokenGenerator = JwtTokenGeneratorBuilder.Build();

        return new RegisterUserUseCase(mapper, passwordEncripter, userReadOnlyRepository, userWhiteOnly, initOfWork, jwtTokenGenerator);
    }
}
