using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class PrivacySettingConfiguration : IEntityTypeConfiguration<PrivacySetting>
{
    public void Configure(EntityTypeBuilder<PrivacySetting> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("privacy_settings");

        entity.HasIndex(e => e.UserId, "idx_user_id").IsUnique();

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.AllowSearchEngines)
            .HasDefaultValueSql("'1'")
            .HasColumnName("allow_search_engines");
        entity.Property(e => e.AnalyticsConsent)
            .HasDefaultValueSql("'1'")
            .HasColumnName("analytics_consent");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.DataSharingConsent)
            .HasDefaultValueSql("'0'")
            .HasColumnName("data_sharing_consent");
        entity.Property(e => e.MarketingConsent)
            .HasDefaultValueSql("'0'")
            .HasColumnName("marketing_consent");
        entity.Property(e => e.ProfileVisibility)
            .HasDefaultValueSql("'public'")
            .HasColumnType("enum('public','friends_only','private')")
            .HasColumnName("profile_visibility");
        entity.Property(e => e.ShowBirthDate)
            .HasDefaultValueSql("'0'")
            .HasColumnName("show_birth_date");
        entity.Property(e => e.ShowEmail)
            .HasDefaultValueSql("'0'")
            .HasColumnName("show_email");
        entity.Property(e => e.ShowPhone)
            .HasDefaultValueSql("'0'")
            .HasColumnName("show_phone");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithOne(p => p.PrivacySetting)
            .HasForeignKey<PrivacySetting>(d => d.UserId)
            .HasConstraintName("privacy_settings_ibfk_1");
    }
}