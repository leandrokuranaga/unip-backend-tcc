using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.MaintenanceAggregate;
using Motorcycle.Domain.MotorcycleAggregate;
using System.ComponentModel;

namespace Motorcycle.Infra.Data.Mapping
{
    public class MaintenanceMap : IEntityTypeConfiguration<MaintenanceDomain>
    {
        public void Configure(EntityTypeBuilder<MaintenanceDomain> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.IdMoto).IsRequired().HasColumnType("int");
            builder.Property(u => u.IdOwner).IsRequired().HasColumnType("int");
            builder.Property(u => u.Service).IsRequired().HasColumnType("varchar(100)");
            builder.Property<DateOnly>(e => e.Date).HasConversion<DateOnlyConverter>().HasColumnType("date").IsRequired();

            builder.HasOne(x => x.Motorcycle)
                .WithMany(x => x.Maintenances)
                .HasForeignKey(x => x.IdMoto);

            builder.HasOne(c => c.User)
                .WithMany(c => c.Maintenance)
                .HasForeignKey(x => x.IdOwner)
                .OnDelete(DeleteBehavior.Restrict); // Modifique para Restrict
            ;
        }
    }
}
