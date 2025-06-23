using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.UserMemberId)
                   .HasColumnName("user_member_id")
                   .IsRequired();

            builder.Property(e => e.Token)
                   .HasColumnName("token")
                   .HasColumnType("text");

            builder.Property(e => e.Expires)
                   .HasColumnName("expires")
                   .HasColumnType("timestamp");

            builder.Property(e => e.Created)
                   .HasColumnName("created")
                   .HasColumnType("timestamp");

            builder.Property(e => e.Revoked)
                   .HasColumnName("revoked")
                   .HasColumnType("timestamp");

            builder.Property(e => e.CreatedAt)
                   .HasColumnName("created_at")
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.Property(e => e.UpdatedAt)
                   .HasColumnName("updated_at")
                   .HasColumnType("timestamp")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP");

            builder.HasOne(e => e.User)
                   .WithMany(u => u.RefreshTokens)
                   .HasForeignKey(e => e.UserMemberId);
        }
    }
}
