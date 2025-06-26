using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class PaymentMethod : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string MethodType { get; set; } = null!;

    public string Provider { get; set; } = null!;

    public string? MaskedNumber { get; set; }

    public string? HolderName { get; set; }

    public sbyte? ExpiryMonth { get; set; }

    public short? ExpiryYear { get; set; }

    public bool? IsDefault { get; set; }

    public bool? IsActive { get; set; }

    public string? ExternalId { get; set; }

    public string? Metadata { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User User { get; set; } = null!;
}
