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

            base.OnModelCreating(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            //var currencyId = Guid.NewGuid();
            //var userId = Guid.NewGuid().ToString();

            //modelBuilder.Entity<Currency>().HasData(new Currency { Id = currencyId, IsDeleted = false, Name = "euro" });

            //modelBuilder.Entity<User>().HasData(
            //                                 new User
            //                                 {
            //                                     Id = userId,
            //                                     Balance = 1000,
            //                                     BirthDate = DateTime.Now.AddYears(-25),
            //                                     IsDeleted = false,
            //                                     CurrencyId = currencyId,
            //                                     UserName = "TestUser"
            //                                 });


            string noneGuidString = "618d2663-fd74-497e-965b-572076e97ca0";
            Guid noneGuid = new Guid(noneGuidString);
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), IsDeleted = false, Name = "EUR" });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), IsDeleted = false, Name = "USD" });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), IsDeleted = false, Name = "BGN" });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id = Guid.NewGuid(), IsDeleted = false, Name = "GBP" });
            modelBuilder.Entity<Currency>().HasData(new Currency { Id =noneGuid , IsDeleted = false, Name = "GBP" });

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
