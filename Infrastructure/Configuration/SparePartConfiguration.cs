using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class SparePartConfiguration : IEntityTypeConfiguration<SparePart>
    {
        public void Configure(EntityTypeBuilder<SparePart> builder)
        {
            builder.ToTable("spare_part");

            builder.HasKey(sp => sp.Id);
            builder.Property(sp => sp.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(sp => sp.Code)
                .HasMaxLength(20)
                .HasColumnName("Code");

            builder.Property(sp => sp.Description)
                .HasMaxLength(50)
                .HasColumnName("Description");

            builder.Property(sp => sp.Stock)
                .HasColumnName("Stock");

            builder.Property(sp => sp.MiniStock)
                .HasColumnName("MiniStock");

            builder.Property(sp => sp.UnitPrice)
                .HasPrecision(18, 2)
                .HasColumnType("decimal")
                .HasColumnName("UnitPrice");

            builder.Property(sp => sp.Category)
                .HasMaxLength(50)
                .HasColumnName("Category");

        }
    }
}