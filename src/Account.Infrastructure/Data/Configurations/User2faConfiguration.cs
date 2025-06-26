using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class User2faConfiguration : IEntityTypeConfiguration<User2fa>
{
    public void Configure(EntityTypeBuilder<User2fa> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("user_2fa");

        entity.HasIndex(e => e.IsEnabled, "idx_is_enabled");

        entity.HasIndex(e => e.UserId, "idx_user_id").IsUnique();

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.BackupCodes)
            .HasColumnType("json")
            .HasColumnName("backup_codes");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.EmailAddress)
            .HasMaxLength(255)
            .HasColumnName("email_address");
        entity.Property(e => e.IsEnabled)
            .HasDefaultValueSql("'0'")
            .HasColumnName("is_enabled");
        entity.Property(e => e.Method)
            .HasDefaultValueSql("'totp'")
            .HasColumnType("enum('totp','sms','email')")
            .HasColumnName("method");
        entity.Property(e => e.PhoneNumber)
            .HasMaxLength(20)
            .HasColumnName("phone_number");
        entity.Property(e => e.SecretKey)
            .HasMaxLength(255)
            .HasColumnName("secret_key");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithOne(p => p.User2fa)
            .HasForeignKey<User2fa>(d => d.UserId)
            .HasConstraintName("user_2fa_ibfk_1");
    }
}