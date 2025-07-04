using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class ServiceOrderConfiguration : IEntityTypeConfiguration<ServiceOrder>
    {
        public void Configure(EntityTypeBuilder<ServiceOrder> builder)
        {
            builder.ToTable("service_order");

            builder.HasKey(so => so.Id);
            builder.Property(so => so.Id)
                .ValueGeneratedOnAdd()
                .IsRequired()
                .HasColumnName("id");

            builder.Property(so => so.VehiclesId)
                .HasColumnName("vehicles_id");

            builder.Property(so => so.TypeServiceId)
                .HasColumnName("type_service_id");

            builder.Property(so => so.StateId)
                .HasColumnName("state_id");

            builder.Property(c => c.EntryDate)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("entry_date");

            builder.Property(c => c.ExitDate)
                    .IsRequired()
                    .HasColumnType("date")
                    .HasColumnName("exit_date");

            // Otros campos
            builder.Property(so => so.IsAuthorized)
                .IsRequired()
                .HasDefaultValue(false)
                .HasColumnName("is_authorized");

            builder.Property(so => so.ClientMessage)
                .HasColumnName("client_message");

            builder.HasOne(so => so.Vehicle)
                .WithMany(v => v.ServiceOrders)
                .HasForeignKey(so => so.VehiclesId);

            builder.HasOne(so => so.TypeService)
                .WithMany(ts => ts.ServiceOrders)
                .HasForeignKey(so => so.TypeServiceId);

            builder.HasOne(so => so.State)
                .WithMany(s => s.ServiceOrders)
                .HasForeignKey(so => so.StateId);

            builder.HasOne(so => so.Invoices)
                .WithOne(i => i.ServiceOrders)
                .HasForeignKey<Invoice>(i => i.ServiceOrderId);

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