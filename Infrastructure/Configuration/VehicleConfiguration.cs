using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
               builder.ToTable("Vehicles");

            // Clave primaria
               builder.HasKey(v => v.Id);

            
               builder.Property(v => v.Brand)
                   .IsRequired()
                   .HasMaxLength(30);

               builder.Property(v => v.Model)
                   .IsRequired()
                   .HasMaxLength(50);

               builder.Property(v => v.VIN)
                   .IsRequired()
                   .HasMaxLength(20);

               builder.HasIndex(v => v.VIN).IsUnique();

               builder.Property(v => v.Mileage)
                   .IsRequired();
              
              builder.Property(v => v.ClientId)
                   .IsRequired()
                   .HasColumnName("ClientId");
       

            // Relación Vehicle -> ServiceOrders (uno a muchos)
                     builder.HasMany(v => v.ServiceOrders)
                   .WithOne(so => so.Vehicle)
                   .HasForeignKey(so => so.VehiclesId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relación muchos a uno hacia Client (se configura solo el lado dependiente aquí)
            builder.HasOne(v => v.Client)
                   .WithMany(c => c.Vehicles)
                   .HasForeignKey(v => v.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
