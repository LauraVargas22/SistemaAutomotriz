using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class RolConfiguration: IEntityTypeConfiguration<Rol>
    {
        public void Configure(EntityTypeBuilder<Rol> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);
            builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(r => r.Description)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasColumnType("date")
                .HasDefaultValueSql("CURRENT_DATE")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasColumnType("date")
                .HasDefaultValueSql("CURRENT_DATE")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}