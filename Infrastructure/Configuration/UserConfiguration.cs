using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);
            builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

            builder.Property(u => u.Name)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("name");

            builder.Property(u => u.LastName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("lastname");

            builder.Property(u => u.UserName)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("username");

            builder.HasIndex(u => u.UserName).IsUnique();

            builder.Property(u => u.Email)
                .HasMaxLength(50)
                .IsRequired()
                .HasColumnName("email");

            builder.HasIndex(u => u.Email).IsUnique();

            builder.Property(u => u.Password)
                .HasMaxLength(20)
                .IsRequired()
                .HasColumnName("password");
                
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
