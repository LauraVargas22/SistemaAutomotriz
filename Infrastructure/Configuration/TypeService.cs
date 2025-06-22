using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TypeServiceConfiguration : IEntityTypeConfiguration<TypeService>
    {
        public void Configure(EntityTypeBuilder<TypeService> builder)
        {
            builder.ToTable("type_service");

            builder.HasKey(ts => ts.Id);
            builder.Property(ts => ts.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(ts => ts.Name)
                .HasMaxLength(40)
                .HasColumnName("name");

            builder.Property(ts => ts.Duration)
                .HasColumnName("duration");

            builder.Property(ts => ts.Price)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("price");

            builder.Property(e => e.CreatedAt)
                .HasColumnName("createdAt")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(e => e.UpdatedAt)
                .HasColumnName("updatedAt")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}