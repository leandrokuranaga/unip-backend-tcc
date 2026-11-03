using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.MotorcycleAggregate;
using Motorcycle.Domain.UserAggregate;

namespace Motorcycle.Infra.Data.Mapping
{
    public class MotorcycleMap : IEntityTypeConfiguration<MotorcycleDomain>
    {
        public void Configure(EntityTypeBuilder<MotorcycleDomain> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Model).IsRequired().HasColumnType("varchar(100)");
            builder.Property(u => u.Status).IsRequired().HasColumnType("bit");
            builder.Property(u => u.IdOwner).IsRequired().HasColumnType("int");

            builder.HasOne(s => s.User)
                .WithMany(x => x.Motorcycles)
                .HasForeignKey(x => x.IdOwner);
        }
    }
}
