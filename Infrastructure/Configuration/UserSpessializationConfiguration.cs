using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configuration
{
    public class UserSpecializationConfiguration: IEntityTypeConfiguration<UserSpecialization>
    {
        public void Configure(EntityTypeBuilder<UserSpecialization> builder)
        {
            builder.ToTable("UserSpecializations");

            // Clave primaria compuesta
            builder.HasKey(us => new { us.SpecializationId, us.UserId });

            // Relaciones

            // Muchos UserSpecialization para un User
            builder.HasOne(us => us.User)
                   .WithMany(u => u.UserSpecializations)
                   .HasForeignKey(us => us.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Muchos UserSpecialization para una Specialization
            builder.HasOne(us => us.Specialization)
                   .WithMany(s => s.UserSpecialization)
                   .HasForeignKey(us => us.SpecializationId)
                   .OnDelete(DeleteBehavior.Cascade);
        } 
    }
}