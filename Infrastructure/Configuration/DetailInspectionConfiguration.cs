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
                .HasColumnName("serviceOrder_id");

            builder.Property(di => di.InspectionId)
                .HasColumnName("inspection_id");

            builder.Property(di => di.Quantity)
                .HasColumnName("quantity");

            builder.HasOne(di => di.ServiceOrder)
                .WithMany(so => so.DetaillInspections)
                .HasForeignKey(di => di.ServiceOrderId);

            builder.HasOne(di => di.Inspection)
                .WithMany(i => i.DetaillInspections)
                .HasForeignKey(di => di.InspectionId);
                
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