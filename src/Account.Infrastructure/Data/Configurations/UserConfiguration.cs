using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .HasColumnName("id");

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(255);
        
        builder.HasIndex(u => u.Email)
            .IsUnique();

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(100);
        
        builder.HasIndex(u => u.Username)
            .IsUnique();

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20);

        builder.Property(u => u.DateOfBirth)
            .HasColumnName("date_of_birth");

        builder.Property(u => u.AvatarUrl)
            .HasColumnName("avatar_url")
            .HasMaxLength(500);

        builder.Property(u => u.EmailVerified)
            .HasColumnName("email_verified")
            .HasDefaultValue(false);

        builder.Property(u => u.PhoneVerified)
            .HasColumnName("phone_verified")
            .HasDefaultValue(false);

        builder.Property(u => u.AccountStatus)
            .HasColumnName("account_status")
            .HasDefaultValue(AccountStatus.Pending)
            .HasConversion<string>();

        builder.Property(u => u.LastLoginAt)
            .HasColumnName("last_login_at");

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");

        builder.Property(u => u.UpdatedAt)
            .HasColumnName("updated_at");

        builder.Property(u => u.DeletedAt)
            .HasColumnName("deleted_at");

        // Relationships
        builder.HasOne(u => u.Profile)
            .WithOne(p => p!.User)
            .HasForeignKey<UserProfile>(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.TwoFactor)
            .WithOne(tf => tf!.User)
            .HasForeignKey<UserTwoFactor>(tf => tf.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.NotificationSettings)
            .WithOne(ns => ns!.User)
            .HasForeignKey<NotificationSettings>(ns => ns.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.PrivacySettings)
            .WithOne(ps => ps!.User)
            .HasForeignKey<PrivacySettings>(ps => ps.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Sessions)
            .WithOne(s => s!.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ActivityLogs)
            .WithOne(al => al!.User)
            .HasForeignKey(al => al.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.PaymentMethods)
            .WithOne(pm => pm!.User)
            .HasForeignKey(pm => pm.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Transactions)
            .WithOne(t => t!.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Tokens)
            .WithOne(t => t!.User)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.ApiKeys)
            .WithOne(ak => ak!.User)
            .HasForeignKey(ak => ak.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
