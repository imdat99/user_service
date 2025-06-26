using System;

namespace Account.Domain.Common;

public abstract class BaseEntity
{
    public string Id { get; protected set; } = string.Empty;
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }
    
    public void MarkAsDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}
