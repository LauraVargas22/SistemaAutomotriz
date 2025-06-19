using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class DetailInspectionConfiguration : IEntityTypeConfiguration<DetailInspection>
    {
        public void Configure(EntityTypeBuilder<DetailInspection> builder)
        {
            builder.ToTable("detaill_inspection");
            builder.HasKey(di => di.Id);
            builder.Property(di => di.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(di => di.ServiceOrderId)
                .HasColumnName("ServiceOrder_id");

            builder.Property(di => di.InspectionId)
                .HasColumnName("Inspection_id");

            builder.Property(di => di.Quantity)
                .HasColumnName("Quantity");
                
            builder.HasOne(di => di.ServiceOrder)
                .WithMany(so => so.DetaillInspections)
                .HasForeignKey(di => di.ServiceOrderId)
                .OnDelete(DeleteBehavior.Cascade);;

            builder.HasOne(di => di.Inspection)
                .WithMany(i => i.DetaillInspections)
                .HasForeignKey(di => di.InspectionId)
                .OnDelete(DeleteBehavior.Cascade);;

        }
    }
}