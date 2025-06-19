using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class SpecializationConfiguration: IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            builder.ToTable("Specializations");

            // Clave primaria
            builder.HasKey(s => s.Id);

            // Propiedades
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(50);

            // Relaciones
            builder.HasMany(s => s.UserSpecialization)
                   .WithOne(us => us.Specialization)
                   .HasForeignKey(us => us.SpecializationId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}