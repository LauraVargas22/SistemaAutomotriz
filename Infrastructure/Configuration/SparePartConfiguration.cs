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
                .HasColumnName("code");

            builder.Property(sp => sp.Description)
                .HasMaxLength(50)
                .HasColumnName("description");

            builder.Property(sp => sp.Stock)
                .HasColumnName("stock");

            builder.Property(sp => sp.MiniStock)
                .HasColumnName("min_stock");

            builder.Property(sp => sp.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("unit_price");

            builder.Property(sp => sp.Category)
                .HasMaxLength(50)
                .HasColumnName("category");

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

                builder.Property(sp => sp.MaxStock)
                     .HasColumnName("max_stock");

                builder.HasMany(sp => sp.OrderDetails)
                    .WithOne(od => od.SpareParts)
                    .HasForeignKey(od => od.SparePartId);


        }
    }
}