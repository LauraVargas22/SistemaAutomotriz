using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
       public class DiagnosticConfiguration : IEntityTypeConfiguration<Diagnostic>
       {
              public void Configure(EntityTypeBuilder<Diagnostic> builder)
              {
                     builder.ToTable("diagnostics");

                     // Clave primaria
                     builder.HasKey(d => d.Id);
                     builder.Property(di => di.Id)
                           .ValueGeneratedOnAdd()
                           .IsRequired()
                           .HasColumnName("id");

                     // Propiedades
                     builder.Property(d => d.Description)
                     .HasColumnName("description");

                     builder.Property(d => d.UserId)
                            .IsRequired()
                            .HasColumnName("user_id");

                     builder.Property(c => c.Date)
                            .IsRequired()
                            .HasColumnType("date")
                            .HasColumnName("date");

                     // Relaciones

                     builder.HasOne(d => d.User)
                            .WithMany(u => u.Diagnostics)
                            .HasForeignKey(d => d.UserId);

                     builder.Property(e => e.CreatedAt)
                            .HasColumnName("createdAt")
                            .HasColumnType("date")
                            .HasDefaultValueSql("CURRENT_DATE")
                            .ValueGeneratedOnAdd();

                     builder.Property(e => e.UpdatedAt)
                            .HasColumnName("updatedAt")
                            .HasColumnType("date")
                            .HasDefaultValueSql("CURRENT_DATE")
                            .ValueGeneratedOnAddOrUpdate();
                            }
       }
}
