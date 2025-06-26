using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("user_sessions");

        entity.HasIndex(e => e.ExpiresAt, "idx_expires_at");

        entity.HasIndex(e => e.IsActive, "idx_is_active");

        entity.HasIndex(e => e.SessionToken, "idx_session_token").IsUnique();

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.DeviceInfo)
            .HasMaxLength(500)
            .HasColumnName("device_info");
        entity.Property(e => e.ExpiresAt)
            .HasColumnType("timestamp")
            .HasColumnName("expires_at");
        entity.Property(e => e.IpAddress)
            .HasMaxLength(45)
            .HasColumnName("ip_address");
        entity.Property(e => e.IsActive)
            .HasDefaultValueSql("'1'")
            .HasColumnName("is_active");
        entity.Property(e => e.SessionToken).HasColumnName("session_token");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserAgent)
            .HasColumnType("text")
            .HasColumnName("user_agent");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("user_sessions_ibfk_1");
    }
}