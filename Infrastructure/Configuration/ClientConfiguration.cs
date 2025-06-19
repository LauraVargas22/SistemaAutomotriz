using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
namespace Infrastructure.Configuration
{
    public class ClientConfiguration: IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.LastName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Email)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(c => c.Phone)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.Property(c => c.Birth)
                     .IsRequired();

            builder.Property(c => c.Identification)
                   .IsRequired()
                   .HasMaxLength(20);

            builder.HasIndex(c => c.Identification).IsUnique();

            // Uno a muchos: Client -> ServiceOrder
            builder.HasMany(c => c.ServiceOrders)
                   .WithOne(so => so.Client)
                   .HasForeignKey(so => so.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
                   
            // Uno a muchos: Client -> Vehicle
            builder.HasMany(c => c.Vehicles)
                   .WithOne(v => v.Client)  
                   .HasForeignKey(v => v.ClientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}