using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Requests;
using FinanceFlow.Application.UseCases.Users.Register;
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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var initOfWork = IUnitOfWorkBuilder.Build();
        var userWhiteOnly = IUserWhiteOnlyRepositoryBuilder.Build();

        return new RegisterUserUseCase(mapper, null, null, userWhiteOnly, initOfWork, null);
    }
}
