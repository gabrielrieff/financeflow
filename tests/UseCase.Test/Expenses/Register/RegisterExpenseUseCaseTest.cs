using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Requests.Expense;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.Register;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Expenses.Register;

public class RegisterExpenseUseCaseTest
{
    [Fact]
    public async Task Sucesso()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpensesJsonBuilder.Build();
        var useCase = CreateUseCase(loggedUser);

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Title.Should().Be(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpensesJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.TITLE_REQUIRED));
    }


    private RegisterExpensesUseCase CreateUseCase(User user)
    {
        var repository = ExpensesWhiteOnlyRepositoryBuilder.Build();
        var mapper = MapperBuilder.Build();
        var initOfWork = UnitOfWorkBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new RegisterExpensesUseCase(repository, initOfWork, mapper, loggedUser);
    }
}
