using System;
using System.Text.Json;
using Account.Domain.Common;

namespace Account.Domain.Entities;

public class UserTwoFactor : BaseEntity
{
    public string UserId { get; private set; } = string.Empty;
    public bool IsEnabled { get; private set; }
    public string? SecretKey { get; private set; }
    public string? BackupCodes { get; private set; } // JSON-serialized backup codes
    public TwoFactorMethod Method { get; private set; }
    public string? PhoneNumber { get; private set; }
    public string? EmailAddress { get; private set; }

    // Navigation property
    public User? User { get; private set; }

    // Required by EF Core
    protected UserTwoFactor() { }

    public UserTwoFactor(string userId)
    {
        Id = Guid.NewGuid().ToString();
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
