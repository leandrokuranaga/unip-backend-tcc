using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motorcycle.Domain.BudgetAggregate;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Domain.UserAggregate;
using Motorcycle.Infra.Data.Mapping;

namespace Motorcycle.Infra.Data
{
    public class ContextDb : DbContext
    {
        public ContextDb(DbContextOptions<ContextDb> options) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersMap());
            modelBuilder.ApplyConfiguration(new MotorcycleMap());
            modelBuilder.ApplyConfiguration(new MaintenanceMap());
            modelBuilder.ApplyConfiguration(new BudgetMap());

            var dateOnlyConverter = new ValueConverter<DateOnly, DateTime>(
            dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue), 
            dateTime => DateOnly.FromDateTime(dateTime));

            modelBuilder.Entity<MaintenanceDomain>()
                .Property(e => e.Date)
                .HasConversion(dateOnlyConverter)
                .HasColumnType("date")
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<UsersDomain> User { get; set; }
        public DbSet<MotorcycleDomain> Motorcycle { get; set; }
        public DbSet<MaintenanceDomain> Maintenance { get; set; }
        public DbSet<BudgetDomain> Budget { get; set; }
    }
}
