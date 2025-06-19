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
            builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");
                   
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
        }
    }
}
