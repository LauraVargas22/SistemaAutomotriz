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