namespace BedeThirteen.Data.Context
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

            this.TransactionTypeConfiguration(modelBuilder);
            this.CurrencyConfiguration(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void CurrencyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }

        private void TransactionTypeConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.HasIndex(e => e.Name).IsUnique();
            });
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Currencies
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = new Guid("618d2663-fd74-497e-965b-572076e97ca0"), Name = "none", IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), Name = "EUR", IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), Name = "USD", IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), Name = "BGN", IsDeleted = false });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), Name = "GBP", IsDeleted = false });

            // Seed Transaction Types
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType { Id = Guid.NewGuid(), Name = "Withdraw", IsDeleted = false });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType { Id = Guid.NewGuid(), Name = "Deposit", IsDeleted = false });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType { Id = Guid.NewGuid(), Name = "Win", IsDeleted = false });
            modelBuilder.Entity<TransactionType>().HasData(new TransactionType { Id = Guid.NewGuid(), Name = "Stake", IsDeleted = false });
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
