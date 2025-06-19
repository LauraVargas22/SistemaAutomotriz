using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class RolConfiguration: IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);
            builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(r => r.Description)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}