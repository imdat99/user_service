using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class Transaction : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public Guid? PaymentMethodId { get; set; }

    public string TransactionType { get; set; } = null!;

    public decimal Amount { get; set; }

    public string? Currency { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public string? ExternalTransactionId { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Metadata { get; set; }

    public DateTime? ProcessedAt { get; set; }

    public virtual PaymentMethod? PaymentMethod { get; set; }

    public virtual User User { get; set; } = null!;
}
