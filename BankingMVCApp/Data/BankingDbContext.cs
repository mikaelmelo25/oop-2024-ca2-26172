//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BankingMVC.Data
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options) : base(options)
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SavingsAccountEntity> SavingsAccounts { get; set; }
        public DbSet<CurrentAccountEntity> CurrentAccounts { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<SavingsAccountTransactionEntity> SavingsAccountTransactions { get; set; }
        public DbSet<CurrentAccountTransactionEntity> CurrentAccountTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>()
                .HasOne(c => c.SavingsAccount)
                .WithOne(sa => sa.Customer)
                .HasForeignKey<SavingsAccountEntity>(sa => sa.CustomerId);

            modelBuilder.Entity<CustomerEntity>()
                .HasOne(c => c.CurrentAccount)
                .WithOne(ca => ca.Customer)
                .HasForeignKey<CurrentAccountEntity>(ca => ca.CustomerId);

            modelBuilder.Entity<SavingsAccountEntity>()
                .HasMany(sa => sa.Transactions)
                .WithOne(sat => sat.SavingsAccount)
                .HasForeignKey(sat => sat.SavingsAccountId);

            modelBuilder.Entity<CurrentAccountEntity>()
                .HasMany(ca => ca.Transactions)
                .WithOne(cat => cat.CurrentAccount)
                .HasForeignKey(cat => cat.CurrentAccountId);

            modelBuilder.Entity<SavingsAccountTransactionEntity>()
                .HasOne(sat => sat.Transaction)
                .WithOne()
                .HasForeignKey<SavingsAccountTransactionEntity>(sat => sat.TransactionId);

            modelBuilder.Entity<CurrentAccountTransactionEntity>()
                .HasOne(cat => cat.CurrentAccount)
                .WithMany(ca => ca.Transactions)
                .HasForeignKey(cat => cat.CurrentAccountId);

            modelBuilder.Entity<CurrentAccountTransactionEntity>()
                .HasOne(cat => cat.Transaction)
                .WithOne()
                .HasForeignKey<CurrentAccountTransactionEntity>(cat => cat.TransactionId);
        }
    }
}