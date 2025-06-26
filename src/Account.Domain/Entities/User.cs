using System;
using Account.Domain.Common;

namespace Account.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string? Username { get; private set; }
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public DateTime? DateOfBirth { get; private set; }
    public string? AvatarUrl { get; private set; }
    public bool EmailVerified { get; private set; }
    public bool PhoneVerified { get; private set; }
    public AccountStatus AccountStatus { get; private set; } = AccountStatus.Pending;
    public DateTime? LastLoginAt { get; private set; }
    
    // Navigation properties
    public UserProfile? Profile { get; private set; }
    public UserTwoFactor? TwoFactor { get; private set; }
    public ICollection<UserSession> Sessions { get; private set; } = new List<UserSession>();
    public ICollection<ActivityLog> ActivityLogs { get; private set; } = new List<ActivityLog>();
    public NotificationSettings? NotificationSettings { get; private set; }
    public PrivacySettings? PrivacySettings { get; private set; }
    public ICollection<PaymentMethod> PaymentMethods { get; private set; } = new List<PaymentMethod>();
    public ICollection<Transaction> Transactions { get; private set; } = new List<Transaction>();
    public ICollection<UserToken> Tokens { get; private set; } = new List<UserToken>();
    public ICollection<ApiKey> ApiKeys { get; private set; } = new List<ApiKey>();

    // Required by EF Core
    protected User() { }

    public User(
        string email, 
        string passwordHash, 
        string firstName, 
        string lastName)
    {
        Id = Guid.NewGuid().ToString();
        Email = email.ToLowerInvariant(); // Always store emails lowercase
        PasswordHash = passwordHash;
        FirstName = firstName;
        LastName = lastName;
        AccountStatus = AccountStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetUsername(string? username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            Username = null;
        }
        else
        {
            Username = username.Trim();
        }
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateContactInfo(string? phone, string? avatarUrl)
    {
        Phone = phone;
        AvatarUrl = avatarUrl;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdatePersonalInfo(string firstName, string lastName, DateTime? dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        UpdatedAt = DateTime.UtcNow;
    }

    public void VerifyEmail()
    {
        EmailVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void VerifyPhone()
    {
        PhoneVerified = true;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateAccountStatus(AccountStatus status)
    {
        AccountStatus = status;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void RecordLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
