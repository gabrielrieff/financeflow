using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Requests.Expense;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.Update;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Enums;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
namespace UseCase.Test.Expenses.Update;
public class UpdateExpenseUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpensesJsonBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, expense);

        var act = async () => await useCase.Execute(expense.Id, request);

        await act.Should().NotThrowAsync();

        expense.Title.Should().Be(request.Title);
        expense.Description.Should().Be(request.Description);
        expense.Create_at.Should().Be(request.Create_at);
        expense.Amount.Should().Be(request.Amount);
        expense.PaymentType.Should().Be((PaymentType)request.PaymentType);

    }

    [Fact]
    public async Task Error_title_Empty()
    {
        var loggedUser = UserBuilder.Build();
        var expense = ExpenseBuilder.Build(loggedUser);

        var request = RequestExpensesJsonBuilder.Build();
        request.Title = string.Empty;

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(expense.Id, request);
        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.TITLE_REQUIRED));

    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var loggedUser = UserBuilder.Build();
        var request = RequestExpensesJsonBuilder.Build();

        var useCase = CreateUseCase(loggedUser);

        var act = async () => await useCase.Execute(id: 1000, request);
        var result = await act.Should().ThrowAsync<NotFoundException>();

        result.Where(ex => ex.GetErrors().Count == 1 &&
                     ex.GetErrors().Contains(ResourceErrorsMessage.EXPENSES_NOT_FOUND));
    }

    private UpdateExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repository = new ExpensesUpdateOnlyRepositoryBuilder().GetById(user, expense).Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new UpdateExpenseUseCase(
                    mapper: mapper,
                    repository: repository,
                    unitOfWork: unitOfWork,
                    loggedUser: loggedUser);
                    }
}
