using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class TelephoneNumbersConfiguration : IEntityTypeConfiguration<TelephoneNumbers>
    {
        public void Configure(EntityTypeBuilder<TelephoneNumbers> builder)
        {
            builder.ToTable("telephone_numbers");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Number)
                .HasColumnName("number")
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(t => t.ClientId)
                .HasColumnName("client_id");

            builder.HasOne(t => t.Client)
                .WithMany(c => c.TelephoneNumbers)
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}