using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder.ToTable("vehicles");

            // Clave primaria
            builder.HasKey(v => v.Id);
            builder.Property(v => v.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(v => v.Brand)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnName("brand");

            builder.Property(v => v.Model)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("model");

            builder.Property(v => v.VIN)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("vin");

            builder.HasIndex(v => v.VIN).IsUnique();

            builder.Property(v => v.Mileage)
                .IsRequired()
                .HasColumnName("mileage");

            builder.Property(v => v.ClientId)
                 .IsRequired()
                 .HasColumnName("client_id");

            // Relación muchos a uno hacia Client (se configura solo el lado dependiente aquí)
            builder.HasOne(c => c.Clients)
                   .WithMany(v => v.Vehicles)
                   .HasForeignKey(c => c.ClientId);

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
