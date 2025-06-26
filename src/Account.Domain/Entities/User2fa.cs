using System;
using System.Collections.Generic;
using System.Text.Json;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class User2fa : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public bool? IsEnabled { get; set; }

    public string? SecretKey { get; set; }

    public string? BackupCodes { get; set; }

    public TwoFactorMethod? Method { get; set; }

    public string? PhoneNumber { get; set; }

    public string? EmailAddress { get; set; }
    public virtual User User { get; set; } = null!;
    // Required by EF Core
    protected User2fa() { }

    public User2fa(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        IsEnabled = false;
        Method = TwoFactorMethod.Totp;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Enable(TwoFactorMethod method, string secretKey)
    {
        IsEnabled = true;
        Method = method;
        SecretKey = secretKey;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Disable()
    {
        IsEnabled = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetBackupCodes(string[] codes)
    {
        BackupCodes = JsonSerializer.Serialize(codes);
        UpdatedAt = DateTime.UtcNow;
    }

    public string[]? GetBackupCodes()
    {
        if (string.IsNullOrEmpty(BackupCodes))
            return null;
        
        return JsonSerializer.Deserialize<string[]>(BackupCodes);
    }

    public void SetPhoneNumber(string? phoneNumber)
    {
        PhoneNumber = phoneNumber;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetEmailAddress(string? emailAddress)
    {
        EmailAddress = emailAddress;
        UpdatedAt = DateTime.UtcNow;
    }
}
public enum TwoFactorMethod
{
    Totp,
    Sms,
    Email
}