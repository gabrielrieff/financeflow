using commonTestUtilities.Entities;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.Report;
using FinanceFlow.Domain.Entities;
using FluentAssertions;

namespace UseCase.Test.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = ExpenseBuilder.Collection(loggedUser);

        var useCase = CreateUseCase(loggedUser, expenses);

        var result = await useCase.Excute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().NotBeNullOrEmpty();

    }

    [Fact]
    public async Task Success_Empty()
    {
        var loggedUser = UserBuilder.Build();

        var useCase = CreateUseCase(loggedUser, new List<Expense>());

        var result = await useCase.Excute(DateOnly.FromDateTime(DateTime.Today));

        result.Should().BeEmpty();
    }


    private GenerateExpensesReportPDFUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var loggedUser = LoggedUserBuilder.Build(user);
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();

        return new GenerateExpensesReportPDFUseCase(repository, loggedUser);

    }
}
