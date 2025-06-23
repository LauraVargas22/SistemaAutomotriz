using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("invoice");

            builder.HasKey(i => i.Id);
            builder.Property(or => or.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(i => i.TotalPrice)
                .HasColumnType("decimal")
                .HasColumnName("total_price");

            builder.Property(i => i.Date)
                .IsRequired()
                .HasColumnName("date");

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

            builder.Property(i => i.Code)
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(i => i.ServiceOrderId)
                .HasColumnName("service_order_id");

            builder.HasOne(i => i.ServiceOrders)
                .WithOne(s => s.Invoices)
                .HasForeignKey<Invoice>(i => i.ServiceOrderId);

        }
    }
}
