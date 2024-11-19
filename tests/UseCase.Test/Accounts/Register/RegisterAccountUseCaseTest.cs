using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Accounts;
using commonTestUtilities.Repositories.Recurrences;
using commonTestUtilities.Requests.Account;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Accounts.Register;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Accounts.Register;

public class RegisterAccountUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestAccountJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Should().Contain("Registro concluído");
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestAccountJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.TITLE_REQUIRED));
    }


    private RegisterAccountUseCase CreateUseCase(User user)
    {
        var readOnlyRepoRecurrences = RecurrencesWhiteOnlyRepositoryBuilder.Build();
        var whiteOnlyRepo = AccountsWhiteOnlyRepositoryBuilder.Build();

        var mapper = MapperBuilder.Build();
        var initOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterAccountUseCase(whiteOnlyRepo, readOnlyRepoRecurrences, initOfWork, mapper, loggedUser);
    }
}
