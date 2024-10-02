using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.GetAll;
using FinanceFlow.Domain.Entities;
using FluentAssertions;

namespace UseCase.Test.Expenses.GetAll;

public class GetAllExpensesUseCaseTest
{

    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);
        var useCase = CreateUseCase(loggedUser, expenses);
        var result = await useCase.Execute();

        result.Should().NotBeNull();
        result.Expenses.Should().NotBeNullOrEmpty().And.AllSatisfy(expense =>
        {
            expense.Id.Should().BeGreaterThan(0);
            expense.Title.Should().NotBeNullOrEmpty();
            expense.Amount.Should().BeGreaterThan(0);
        });
    }

    private GetAllExpensesUseCase CreateUseCase(User user, List<Expense> expense)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetAll(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new GetAllExpensesUseCase(repositories: repository, mapper: mapper, loggedUser: loggedUser);
    }
}
