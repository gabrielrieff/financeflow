using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.GetAll;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Expenses.GetById;

public class GetByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);
        var result = await useCase.Execute(expenses.Id);

        result.Should().NotBeNull();
        result.Id.Should().Be(expenses.Id);
        result.Title.Should().Be(expenses.Title);
        result.Description.Should().Be(expenses.Description);
        result.Create_at.Should().Be(expenses.Create_at);
        result.Amount.Should().Be(expenses.Amount);
        result.PaymentType.Should().Be((FinanceFlow.Communication.Enums.PaymentsType)expenses.PaymentType);
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser);
        var act = async () => await useCase.Execute(id: 1000);

        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
             ex.GetErrors().Contains(ResourceErrorsMessage.EXPENSES_NOT_FOUND));

    }

    private GetExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new GetExpenseUseCase(repositories: repository, mapper: mapper, loggedUser: loggedUser);
    }
}
