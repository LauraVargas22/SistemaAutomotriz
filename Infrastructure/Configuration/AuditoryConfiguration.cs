using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class AuditoryConfiguration: IEntityTypeConfiguration<Auditory>
    {
        public void Configure (EntityTypeBuilder<Auditory> builder)
        {
            builder.ToTable("Auditories");

            // Clave primaria
            builder.HasKey(a => a.Id);

            // Propiedades

              builder.Property(a => a.Entity)
                   .IsRequired()
                   .HasMaxLength(50);

              builder.Property(a => a.Date)
                   .IsRequired();

              builder.Property(a => a.TypeAction)
                   .IsRequired()
                   .HasConversion<string>();

              builder.Property(a => a.UserId)
                   .IsRequired()
                   .HasColumnName("UserId");

            // Relaciones
            builder.HasOne(a => a.User)
                   .WithMany(u => u.Auditories)
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}