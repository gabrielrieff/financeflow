using commonTestUtilities.Entities;
using commonTestUtilities.Mapper;
using commonTestUtilities.Repositories;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.DeleteById;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Exception.ExceptionBase;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace UseCase.Test.Expenses.DeleteById;

public class DeleteByIdUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Build(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);
        var act = async () =>  await useCase.Execute(expenses.Id);

        await act.Should().NotThrowAsync();
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

    private DeleteExpenseUseCase CreateUseCase(User user, Expense? expense = null)
    {
        var repositoryReadOnly = new ExpensesReadOnlyRepositoryBuilder().GetById(user, expense).Build();
        var repositoryWhiteOnly = ExpensesWhiteOnlyRepositoryBuilder.Build();

        var unitOfWork = UnitOfWorkBuilder.Build();
        var mapper = MapperBuilder.Build();
        var loggedUser = LoggedUserBuilder.Build(user);


        return new DeleteExpenseUseCase(
            repositoryReadOnly: repositoryReadOnly,
            repositoryWhiteOnly: repositoryWhiteOnly,
            unitOfWork: unitOfWork,
            loggedUser: loggedUser);
    }
}
