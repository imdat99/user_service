using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("payment_methods");

        entity.HasIndex(e => e.DeletedAt, "idx_deleted_at");

        entity.HasIndex(e => e.IsActive, "idx_is_active");

        entity.HasIndex(e => e.IsDefault, "idx_is_default");

        entity.HasIndex(e => e.MethodType, "idx_method_type");

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.DeletedAt)
            .HasColumnType("timestamp")
            .HasColumnName("deleted_at");
        entity.Property(e => e.ExpiryMonth)
            .HasColumnType("tinyint(4)")
            .HasColumnName("expiry_month");
        entity.Property(e => e.ExpiryYear)
            .HasColumnType("smallint(6)")
            .HasColumnName("expiry_year");
        entity.Property(e => e.ExternalId)
            .HasMaxLength(255)
            .HasColumnName("external_id");
        entity.Property(e => e.HolderName)
            .HasMaxLength(255)
            .HasColumnName("holder_name");
        entity.Property(e => e.IsActive)
            .HasDefaultValueSql("'1'")
            .HasColumnName("is_active");
        entity.Property(e => e.IsDefault)
            .HasDefaultValueSql("'0'")
            .HasColumnName("is_default");
        entity.Property(e => e.MaskedNumber)
            .HasMaxLength(20)
            .HasColumnName("masked_number");
        entity.Property(e => e.Metadata)
            .HasColumnType("json")
            .HasColumnName("metadata");
        entity.Property(e => e.MethodType)
            .HasColumnType("enum('credit_card','debit_card','paypal','bank_transfer','digital_wallet')")
            .HasColumnName("method_type");
        entity.Property(e => e.Provider)
            .HasMaxLength(50)
            .HasColumnName("provider");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.User).WithMany(p => p.PaymentMethods)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("payment_methods_ibfk_1");
    }
}