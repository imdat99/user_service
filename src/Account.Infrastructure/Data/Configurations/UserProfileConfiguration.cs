using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
{
    public void Configure(EntityTypeBuilder<UserProfile> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("user_profiles");

        entity.HasIndex(e => e.ProfileVisibility, "idx_profile_visibility");

        entity.HasIndex(e => e.UserId, "idx_user_id").IsUnique();

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Bio)
            .HasColumnType("text")
            .HasColumnName("bio");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.Gender)
            .HasColumnType("enum('male','female','other','prefer_not_to_say')")
            .HasColumnName("gender");
        entity.Property(e => e.Language)
            .HasMaxLength(10)
            .HasDefaultValueSql("'en'")
            .HasColumnName("language");
        entity.Property(e => e.Location)
            .HasMaxLength(255)
            .HasColumnName("location");
        entity.Property(e => e.ProfileVisibility)
            .HasDefaultValueSql("'public'")
            .HasColumnType("enum('public','friends_only','private')")
            .HasColumnName("profile_visibility");
        entity.Property(e => e.Timezone)
            .HasMaxLength(50)
            .HasDefaultValueSql("'UTC'")
            .HasColumnName("timezone");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");
        entity.Property(e => e.Website)
            .HasMaxLength(255)
            .HasColumnName("website");

        entity.HasOne(d => d.User).WithOne(p => p.UserProfile)
            .HasForeignKey<UserProfile>(d => d.UserId)
            .HasConstraintName("user_profiles_ibfk_1");
    }
}