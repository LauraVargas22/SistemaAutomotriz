using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("state");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(s => s.Name)
                .HasMaxLength(25)
                .HasColumnName("name");

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