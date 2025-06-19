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

            builder.Property(r => r.Description)
                   .IsRequired()
                   .HasMaxLength(50);

            // Uno a muchos: Rol -> UserRol
            builder.HasMany(r => r.UserRoles)
                   .WithOne(ur => ur.Rol)  
                   .HasForeignKey(ur => ur.RolId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}