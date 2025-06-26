using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
{
    public void Configure(EntityTypeBuilder<UserToken> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("user_tokens");

        entity.HasIndex(e => e.ExpiresAt, "idx_expires_at");

        entity.HasIndex(e => e.IsRevoked, "idx_is_revoked");

        entity.HasIndex(e => e.Jti, "idx_jti").IsUnique();

        entity.HasIndex(e => e.ParentTokenId, "idx_parent_token");

        entity.HasIndex(e => e.TokenType, "idx_token_type");

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.HasIndex(e => new { e.UserId, e.TokenType, e.IsRevoked }, "idx_user_tokens");

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
        entity.Property(e => e.IsRevoked)
            .HasDefaultValueSql("'0'")
            .HasColumnName("is_revoked");
        entity.Property(e => e.Jti).HasColumnName("jti");
        entity.Property(e => e.LastUsedAt)
            .HasColumnType("timestamp")
            .HasColumnName("last_used_at");
        entity.Property(e => e.ParentTokenId).HasColumnName("parent_token_id");
        entity.Property(e => e.RevokedAt)
            .HasColumnType("timestamp")
            .HasColumnName("revoked_at");
        entity.Property(e => e.TokenHash)
            .HasMaxLength(255)
            .HasColumnName("token_hash");
        entity.Property(e => e.TokenType)
            .HasColumnType("enum('access_token','refresh_token')")
            .HasColumnName("token_type");
        entity.Property(e => e.UserAgent)
            .HasColumnType("text")
            .HasColumnName("user_agent");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.ParentToken).WithMany(p => p.InverseParentToken)
            .HasForeignKey(d => d.ParentTokenId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("user_tokens_ibfk_2");

        entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("user_tokens_ibfk_1");
    }
}