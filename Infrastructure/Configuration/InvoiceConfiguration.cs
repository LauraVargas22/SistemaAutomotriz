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
        }
    }
}
