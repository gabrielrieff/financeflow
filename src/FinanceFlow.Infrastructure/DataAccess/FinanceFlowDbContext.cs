using FinanceFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess;

internal class FinanceFlowDbContext : DbContext
{

    public FinanceFlowDbContext(DbContextOptions options) : base(options){}

    public DbSet<Expense> Expenses { get; set; }

}
