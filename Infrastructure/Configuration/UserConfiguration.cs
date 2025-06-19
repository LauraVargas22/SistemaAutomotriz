using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .HasMaxLength(50);

            builder.Property(u => u.UserName)
                .HasMaxLength(50);

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.Email)
                .HasMaxLength(50);
            
            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password)
                .HasMaxLength(20);

            // Uno a muchos: User -> Auditories
            builder.HasMany(u => u.Auditories)
                   .WithOne(a => a.User)    
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Uno a muchos: User -> Diagnostics
            builder.HasMany(u => u.Diagnostics)
                   .WithOne(d => d.User)    
                   .HasForeignKey(d => d.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Muchos a muchos: User <-> UserRol
            builder.HasMany(u => u.UserRols)
                   .WithOne(ur => ur.User)  
                   .HasForeignKey(ur => ur.UserId);

            // Muchos a muchos: User <-> UserSpessialization
            builder.HasMany(u => u.UserSpecializations)
                   .WithOne(us => us.User)  
                   .HasForeignKey(us => us.UserId);
        }
    }
}
