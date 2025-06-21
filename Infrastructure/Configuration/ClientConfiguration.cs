using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;
namespace Infrastructure.Configuration
{
       public class ClientConfiguration : IEntityTypeConfiguration<Client>
       {
              public void Configure(EntityTypeBuilder<Client> builder)
              {
                     builder.ToTable("clients");

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
                            .IsRequired()
                            .HasColumnType("timestamp");

                     builder.Property(c => c.Identification)
                            .IsRequired()
                            .HasMaxLength(20);

                     builder.HasIndex(c => c.Identification).IsUnique();

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