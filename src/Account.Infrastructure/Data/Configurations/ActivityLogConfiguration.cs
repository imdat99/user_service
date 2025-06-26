using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("activity_logs");

        entity.HasIndex(e => e.ActivityType, "idx_activity_type");

        entity.HasIndex(e => e.CreatedAt, "idx_created_at");

        entity.HasIndex(e => new { e.UserId, e.ActivityType, e.CreatedAt }, "idx_user_activity");

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.ActivityType)
            .HasColumnType("enum('login','logout','profile_update','password_change','email_change','phone_change','2fa_enable','2fa_disable','payment_add','payment_remove','transaction')")
            .HasColumnName("activity_type");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.Description)
            .HasColumnType("text")
            .HasColumnName("description");
        entity.Property(e => e.IpAddress)
            .HasMaxLength(45)
            .HasColumnName("ip_address");
        entity.Property(e => e.Metadata)
            .HasColumnType("json")
            .HasColumnName("metadata");
        entity.Property(e => e.UserAgent)
            .HasColumnType("text")
            .HasColumnName("user_agent");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("activity_logs_ibfk_1");
    }
}