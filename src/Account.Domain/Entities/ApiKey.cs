using System;
using System.Collections.Generic;
using Account.Shared.Common;
using Account.Shared.Common;
namespace Account.Domain.Entities;

public partial class ApiKey : AggregateRoot
{
    public Guid UserId { get; set; }

    public string KeyName { get; set; } = null!;

    public string ApiKeyHash { get; set; } = null!;

    public string ApiKeyPrefix { get; set; } = null!;

    public string? Permissions { get; set; }

    public int? RateLimitPerMinute { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }

    public virtual User User { get; set; } = null!;
    // Required by EF Core
    protected ApiKey() { }
}
