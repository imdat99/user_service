using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Domain.Entities;

namespace Account.Infrastructure.Data.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> entity)
    {
        entity.HasKey(e => e.Id).HasName("PRIMARY");

        entity.ToTable("transactions");

        entity.HasIndex(e => e.CreatedAt, "idx_created_at");

        entity.HasIndex(e => e.PaymentMethodId, "idx_payment_method_id");

        entity.HasIndex(e => e.Status, "idx_status");

        entity.HasIndex(e => e.TransactionType, "idx_transaction_type");

        entity.HasIndex(e => e.UserId, "idx_user_id");

        entity.HasIndex(e => new { e.UserId, e.CreatedAt }, "idx_user_transactions").IsDescending(false, true);

        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.Amount)
            .HasPrecision(15, 2)
            .HasColumnName("amount");
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("created_at");
        entity.Property(e => e.Currency)
            .HasMaxLength(3)
            .HasDefaultValueSql("'USD'")
            .HasColumnName("currency");
        entity.Property(e => e.Description)
            .HasColumnType("text")
            .HasColumnName("description");
        entity.Property(e => e.ExternalTransactionId)
            .HasMaxLength(255)
            .HasColumnName("external_transaction_id");
        entity.Property(e => e.Metadata)
            .HasColumnType("json")
            .HasColumnName("metadata");
        entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
        entity.Property(e => e.ProcessedAt)
            .HasColumnType("timestamp")
            .HasColumnName("processed_at");
        entity.Property(e => e.ReferenceNumber)
            .HasMaxLength(100)
            .HasColumnName("reference_number");
        entity.Property(e => e.Status)
            .HasDefaultValueSql("'pending'")
            .HasColumnType("enum('pending','completed','failed','cancelled','refunded')")
            .HasColumnName("status");
        entity.Property(e => e.TransactionType)
            .HasColumnType("enum('payment','refund','subscription','purchase','withdrawal')")
            .HasColumnName("transaction_type");
        entity.Property(e => e.UpdatedAt)
            .ValueGeneratedOnAddOrUpdate()
            .HasDefaultValueSql("current_timestamp()")
            .HasColumnType("timestamp")
            .HasColumnName("updated_at");
        entity.Property(e => e.UserId).HasColumnName("user_id");

        entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Transactions)
            .HasForeignKey(d => d.PaymentMethodId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("transactions_ibfk_2");

        entity.HasOne(d => d.User).WithMany(p => p.Transactions)
            .HasForeignKey(d => d.UserId)
            .HasConstraintName("transactions_ibfk_1");
    }
}