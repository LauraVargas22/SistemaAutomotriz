using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
     public class AuditoryConfiguration : IEntityTypeConfiguration<Auditory>
     {
          public void Configure(EntityTypeBuilder<Auditory> builder)
          {
               builder.ToTable("auditories");

               // Clave primaria
               builder.HasKey(a => a.Id);
               builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

               // Propiedades

               builder.Property(a => a.Entity)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnName("entity");

               builder.Property(a => a.Date)
                    .HasColumnName("date")
                    .HasColumnType("timestamp")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .ValueGeneratedOnAddOrUpdate();

               builder.Property(a => a.TypeAction)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasColumnName("type_action");

               builder.Property(a => a.UserId)
                    .IsRequired()
                    .HasColumnName("user_id");

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

               // Relaciones
               builder.HasOne(a => a.User)
                      .WithMany(u => u.Auditories)
                      .HasForeignKey(a => a.UserId);
          }
     }
}