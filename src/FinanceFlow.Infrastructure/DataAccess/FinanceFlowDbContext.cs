using FinanceFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess;

public class FinanceFlowDbContext : DbContext
{

    public FinanceFlowDbContext(DbContextOptions options) : base(options){}

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Recurrence> Recurrences { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tag>().ToTable("Tags");
    }

}
