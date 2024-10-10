using commonTestUtilities.Entities;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Users;
using commonTestUtilities.Requests.User;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Users.UpdateProfile;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Users.UpdateProfile;

public class UpdateProfileUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var request = RequestUpdateProfileJsonBuilder.Build();
        var user = UserBuilder.Build();

        var useCase = CreateUseCase(user);
        var act = async () => await useCase.Execute(request);

        await act.Should().NotThrowAsync();

        user.Name.Should().Be(request.Name);
        user.Email.Should().Be(request.Email);
    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestUpdateProfileJsonBuilder.Build();
        var user = UserBuilder.Build();

        request.Name = string.Empty;

        var useCase = CreateUseCase(user);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.NAME_REQUIRED));
    }

    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestUpdateProfileJsonBuilder.Build();
        var user = UserBuilder.Build();


        var useCase = CreateUseCase(user, request.Email);
        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.EMAIL_EXIST));
    }

    private UpdateProfileUseCase CreateUseCase(User user, string? email = null)
    {
        var userUpdateOnlyRepository = UserUpdateOnlyRepositoryBuilder.Build(user);
        var userReadOnlyRepository = new UserReadOnlyRepositoryBuilder();
        var initOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            userReadOnlyRepository.ExistActiveUserWithEmail(email);
        }

        return new UpdateProfileUseCase(
            readOnlyRepository: userReadOnlyRepository.Build(),
            repository: userUpdateOnlyRepository,
            initOfWork,
            loggedUser);
    }
}
