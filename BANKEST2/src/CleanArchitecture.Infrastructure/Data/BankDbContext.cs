using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Core.Entities;
using Ardalis.EFCore.Extensions;
using BANKEST2.Core.Entities;

namespace CleanArchitecture.Infrastructure.Data
{
    public class BankDbContext : DbContext
    {

        public BankDbContext(DbContextOptions<BankDbContext> options  )
            : base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<TransactionLog> TransactionLog { get; set; }
        public DbSet<TestsHistory> TestsHistory { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Accounts>().ToTable("Accounts").HasKey("AccountId");
            modelBuilder.Entity<TransactionLog>().ToTable("TransactionLog").HasKey("Id");
            modelBuilder.Entity<TestsHistory>().ToTable("TestsHistory").HasKey("Id");

        }



        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}    