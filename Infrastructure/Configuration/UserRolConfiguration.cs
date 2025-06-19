using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    public class UserRolConfiguration : IEntityTypeConfiguration<UserRol>
    {
        public void Configure(EntityTypeBuilder<UserRol> builder)
        {
            builder.ToTable("UserRoles");

            // Clave primaria compuesta
            builder.HasKey(ur => new { ur.UserId, ur.RolId });

            // Relaciones

            // Muchos UserRol para un User
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRols)
                   .HasForeignKey(ur => ur.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Muchos UserRol para un Rol
            builder.HasOne(ur => ur.Rol)
                   .WithMany(r => r.UserRoles)
                   .HasForeignKey(ur => ur.RolId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
