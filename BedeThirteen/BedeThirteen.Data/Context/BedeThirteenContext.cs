namespace BedeThirteen.Data.Context
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using BedeThirteen.Data.Models;
    using BedeThirteen.Data.Models.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class BedeThirteenContext : DbContext
    {
        public BedeThirteenContext()
        {
        }

        public BedeThirteenContext(DbContextOptions contextOptions)
            : base(contextOptions)
        {
        }

        public DbSet<User> Users { get; set; }

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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(@"Server=.;Database=bedeThirteen;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var currencyId = Guid.NewGuid();
            var userId = Guid.NewGuid().ToString();

            modelBuilder.Entity<Currency>().HasData(new Currency { Id = currencyId, IsDeleted = false, Name = "euro" });

            modelBuilder.Entity<User>().HasData(
                                             new User
                                             {
                                                 Id = userId,
                                                 Balance = 1000,
                                                 BirthDate = DateTime.Now.AddYears(-25),
                                                 IsDeleted = false,
                                                 CurrencyId = currencyId,
                                                 UserName = "TestUser"
                                             });

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
