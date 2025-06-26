using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class ApiKeyConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("api_keys");

        entity.HasIndex(e => e.ApiKeyHash, "api_key_hash").IsUnique();

        entity.HasIndex(e => e.ApiKeyPrefix, "idx_api_key_prefix");

        entity.HasIndex(e => e.DeletedAt, "idx_deleted_at");

        entity.HasIndex(e => e.ExpiresAt, "idx_expires_at");

        entity.HasIndex(e => e.IsActive, "idx_is_active");

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.ApiKeyHash).HasColumnName("api_key_hash");
        entity.Property(e => e.ApiKeyPrefix)
            .HasMaxLength(20)
            .HasColumnName("api_key_prefix");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.DeletedAt)
            .HasColumnType("timestamp")
            .HasColumnName("deleted_at");
        entity.Property(e => e.ExpiresAt)
            .HasColumnType("timestamp")
            .HasColumnName("expires_at");
        entity.Property(e => e.IsActive)
            .HasDefaultValueSql("'1'")
            .HasColumnName("is_active");
        entity.Property(e => e.KeyName)
            .HasMaxLength(100)
            .HasColumnName("key_name");
        entity.Property(e => e.LastUsedAt)
            .HasColumnType("timestamp")
            .HasColumnName("last_used_at");
        entity.Property(e => e.Permissions)
            .HasColumnType("json")
            .HasColumnName("permissions");
        entity.Property(e => e.RateLimitPerMinute)
            .HasDefaultValueSql("'1000'")
            .HasColumnType("int(11)")
            .HasColumnName("rate_limit_per_minute");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithMany(p => p.ApiKeys)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("api_keys_ibfk_1");
    }
}