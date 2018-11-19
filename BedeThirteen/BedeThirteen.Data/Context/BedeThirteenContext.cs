﻿namespace BedeThirteen.Data.Context
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Data.Models.Contracts;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    public class BedeThirteenContext : IdentityDbContext<User>
    {
        public BedeThirteenContext()
        {
        }

        public BedeThirteenContext(DbContextOptions<BedeThirteenContext> contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<CreditCard> CreditCards { get; set; }

        public DbSet<Currency> Currencies { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<TransactionType> TransactionTypes { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            this.ApplyAuditInfoRules();

            return await this.SaveChangesAsync(true, cancellationToken);
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>().HasData(new Currency { Name = "EUR", ConversionRateToUSD = 1, IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Name = "USD", ConversionRateToUSD = 1, IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Name = "BGN", ConversionRateToUSD = 1, IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Name = "GBP", ConversionRateToUSD = 1, IsDeleted = false });
        }

        private void ApplyAuditInfoRules()
        {
            var newlyCreatedEntities = this.ChangeTracker.Entries()
                .Where(e => e.Entity is IEditable
                       && ((e.State == EntityState.Added)
                       || (e.State == EntityState.Modified)));

            foreach (var entry in newlyCreatedEntities)
            {
                var entity = (IEditable)entry.Entity;

                if (entry.State == EntityState.Added && entity.CreatedOn == null)
                {
                    entity.CreatedOn = DateTime.Now;
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}