using commonTestUtilities.Entities;
using commonTestUtilities.Repositories.Expenses;
using commonTestUtilities.Services.LoggedUser;
using FinanceFlow.Application.UseCases.Expenses.Report;
using FinanceFlow.Domain.Entities;
using FluentAssertions;

namespace UseCase.Test.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCaseTest
{
    [Fact]
    public async Task Success()
    {
        var loggedUser = UserBuilder.Build();
        var expenses = AccountBuilder.Collection(loggedUser);

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


    private GenerateExpensesReportExcelUseCase CreateUseCase(User user, List<Expense> expenses)
    {
        var repository = new ExpensesReadOnlyRepositoryBuilder().FilterByMonth(user, expenses).Build();
        var loggedUser = LoggedUserBuilder.Build(user);

        return new GenerateExpensesReportExcelUseCase(repository, loggedUser);

    }
}
