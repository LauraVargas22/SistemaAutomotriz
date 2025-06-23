using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class TypeVehicleConfiguration : IEntityTypeConfiguration<TypeVehicle>
    {
        public void Configure(EntityTypeBuilder<TypeVehicle> builder)
        {
            builder.ToTable("type_vehicle");

            builder.HasKey(ts => ts.Id);
            builder.Property(ts => ts.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(ts => ts.Name)
                .HasMaxLength(40)
                .HasColumnName("name");

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