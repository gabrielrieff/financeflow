using commonTestUtilities.Cryptography;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Repositories;
using commonTestUtilities.Requests.User;
using commonTestUtilities.Token;
using FinanceFlow.Application.UseCases.Users.Register;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using FinanceFlow.Application.UseCases.Users.GetProfile;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Domain.Entities;
using commonTestUtilities.Entities;

namespace UseCase.Test.Users.GetProfile;

public class GetProfileUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Name.Should().Be(user.Name);
        result.Email.Should().Be(user.Email);
    }

    private GetProfileUseCase CreateUseCase(User user)
    {
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GetProfileUseCase(loggedUser, mapper);
    }
}
