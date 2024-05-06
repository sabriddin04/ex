using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
        .HasMany(x => x.Accounts)
        .WithOne(x => x.Customer)
        .HasForeignKey(x => x.CustomerId);

        modelBuilder.Entity<Account>()
        .HasMany(x => x.Transactions)
        .WithOne(x => x.FromAccount)
        .HasForeignKey(x => x.FromAccountId);

        base.OnModelCreating(modelBuilder);
    }

}
