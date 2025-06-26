using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class UserSession : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string SessionToken { get; set; } = null!;

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public bool? IsActive { get; set; }

    public DateTime ExpiresAt { get; set; }
    public virtual User User { get; set; } = null!;
    // Required by EF Core
    protected UserSession() { }

    public UserSession(
        Guid userId, 
        string sessionToken, 
        DateTime expiresAt, 
        string? deviceInfo = null, 
        string? ipAddress = null, 
        string? userAgent = null)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        SessionToken = sessionToken;
        ExpiresAt = expiresAt;
        DeviceInfo = deviceInfo;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void ExtendSession(DateTime newExpiresAt)
    {
        ExpiresAt = newExpiresAt;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsExpired()
    {
        return DateTime.UtcNow > ExpiresAt;
    }
}
