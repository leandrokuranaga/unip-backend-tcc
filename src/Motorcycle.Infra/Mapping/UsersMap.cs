using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.UserAggregate;

namespace Motorcycle.Infra.Data.Mapping
{
    public class UsersMap : IEntityTypeConfiguration<UsersDomain>
    {
        public void Configure(EntityTypeBuilder<UsersDomain> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name).IsRequired().HasColumnType("varchar(100)");
            builder.Property(u => u.Admin).IsRequired().HasColumnType("bit");
            builder.Property(u => u.Email).IsRequired().HasColumnType("varchar(100)");
            builder.Property(u =>  u.Password).IsRequired().HasColumnType("varchar(100)");
            
        }
    }
}
