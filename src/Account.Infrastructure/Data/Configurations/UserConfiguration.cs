using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("users");

        entity.HasIndex(e => e.Email, "email").IsUnique();

        entity.HasIndex(e => e.AccountStatus, "idx_account_status");

        entity.HasIndex(e => e.CreatedAt, "idx_created_at");

        entity.HasIndex(e => e.DeletedAt, "idx_deleted_at");

        entity.HasIndex(e => e.Username, "idx_username").IsUnique();

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.AccountStatus)
            .HasDefaultValueSql("'pending'")
            .HasColumnType("enum('active','inactive','suspended','pending')")
            .HasColumnName("account_status");
        entity.Property(e => e.AvatarUrl)
            .HasMaxLength(500)
            .HasColumnName("avatar_url");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
        entity.Property(e => e.DeletedAt)
            .HasColumnType("timestamp")
            .HasColumnName("deleted_at");
        entity.Property(e => e.Email).HasColumnName("email");
        entity.Property(e => e.EmailVerified)
            .HasDefaultValueSql("'0'")
            .HasColumnName("email_verified");
        entity.Property(e => e.FirstName)
            .HasMaxLength(100)
            .HasColumnName("first_name");
        entity.Property(e => e.LastLoginAt)
            .HasColumnType("timestamp")
            .HasColumnName("last_login_at");
        entity.Property(e => e.LastName)
            .HasMaxLength(100)
            .HasColumnName("last_name");
        entity.Property(e => e.PasswordHash)
            .HasMaxLength(255)
            .HasColumnName("password_hash");
        entity.Property(e => e.Phone)
            .HasMaxLength(20)
            .HasColumnName("phone");
        entity.Property(e => e.PhoneVerified)
            .HasDefaultValueSql("'0'")
            .HasColumnName("phone_verified");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.Username)
            .HasMaxLength(100)
            .HasColumnName("username");
    }
}
