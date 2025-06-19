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
            builder.Property(di => di.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired()
                   .HasColumnName("id");

            // Propiedades
            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(50);
        }
    }
}