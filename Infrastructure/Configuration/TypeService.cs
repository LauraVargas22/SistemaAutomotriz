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
                .HasColumnName("Name");

            builder.Property(ts => ts.Duration)
                .HasColumnName("Duration");

            builder.Property(ts => ts.Price)
                .HasColumnType("decimal")
                .HasColumnName("Price");
        }
    }
}