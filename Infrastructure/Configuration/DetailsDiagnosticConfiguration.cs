using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class DetailsDiagnosticConfiguration: IEntityTypeConfiguration<DetailsDiagnostic>
    {
       public void Configure(EntityTypeBuilder<DetailsDiagnostic> builder)
        {
            builder.ToTable("DetailsDiagnostics");

            // Clave primaria compuesta
            builder.HasKey(dd => new { dd.ServiceOrderId, dd.DiagnosticId });

            // Relaciones

            // Muchos DetailsDiagnostic para un ServiceOrder
            builder.HasOne(dd => dd.ServiceOrder)
                   .WithMany(so => so.DetailsDiagnostics)
                   .HasForeignKey(dd => dd.ServiceOrderId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Muchos UserSpessialization para una Spessialization
            builder.HasOne(dd => dd.Diagnostic)
                   .WithMany(d => d.DetailsDiagnostics)
                   .HasForeignKey(dd => dd.DiagnosticId)
                   .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}