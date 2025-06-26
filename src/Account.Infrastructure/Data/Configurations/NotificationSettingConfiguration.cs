using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class NotificationSettingConfiguration : IEntityTypeConfiguration<NotificationSetting>
{
    public void Configure(EntityTypeBuilder<NotificationSetting> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("notification_settings");

        entity.HasIndex(e => e.UserId, "idx_user_id").IsUnique();

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.EmailNotifications)
            .HasDefaultValueSql("'1'")
            .HasColumnName("email_notifications");
        entity.Property(e => e.LoginAlerts)
            .HasDefaultValueSql("'1'")
            .HasColumnName("login_alerts");
        entity.Property(e => e.MarketingEmails)
            .HasDefaultValueSql("'0'")
            .HasColumnName("marketing_emails");
        entity.Property(e => e.PaymentNotifications)
            .HasDefaultValueSql("'1'")
            .HasColumnName("payment_notifications");
        entity.Property(e => e.ProfileUpdates)
            .HasDefaultValueSql("'1'")
            .HasColumnName("profile_updates");
        entity.Property(e => e.PushNotifications)
            .HasDefaultValueSql("'1'")
            .HasColumnName("push_notifications");
        entity.Property(e => e.SecurityAlerts)
            .HasDefaultValueSql("'1'")
            .HasColumnName("security_alerts");
        entity.Property(e => e.SmsNotifications)
            .HasDefaultValueSql("'0'")
            .HasColumnName("sms_notifications");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithOne(p => p.NotificationSetting)
            .HasForeignKey<NotificationSetting>(d => d.UserId)
            .HasConstraintName("notification_settings_ibfk_1");
    }
}