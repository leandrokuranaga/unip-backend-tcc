using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.BudgetAggregate;

namespace Motorcycle.Infra.Data.Mapping
{
    public class BudgetMap : IEntityTypeConfiguration<BudgetDomain>
    {
        public void Configure(EntityTypeBuilder<BudgetDomain> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(z => z.Price);
            builder.Property(z => z.Component);
            builder.Property(z => z.IdUsers);
            builder.Property(z => z.IdMotorcycle);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Budget)
                .HasForeignKey(x => x.IdUsers)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(x => x.Motorcycle)
                .WithMany(x => x.Budget)
                .HasForeignKey(x => x.IdMotorcycle);
        }
    }
}
