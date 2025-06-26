using Account.Shared.Common;
namespace Account.Domain.Entities;
public enum AccountStatus
{
    Pending,
    Active,
    Inactive,
    Suspended
}

public partial class User : AggregateRoot
{
    public string Email { get; set; } = null!;

    public string? Username { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Phone { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? AvatarUrl { get; set; }

    public bool? EmailVerified { get; set; }

    public bool? PhoneVerified { get; set; }

    public AccountStatus AccountStatus { get; private set; } = AccountStatus.Pending;

    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual ICollection<ApiKey> ApiKeys { get; set; } = new List<ApiKey>();

    public virtual NotificationSetting? NotificationSetting { get; set; }

    public virtual ICollection<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();

    public virtual PrivacySetting? PrivacySetting { get; set; }

    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    public virtual User2fa? User2fa { get; set; }

    public virtual UserProfile? UserProfile { get; set; }

    public virtual ICollection<UserSession> UserSessions { get; set; } = new List<UserSession>();

    public virtual ICollection<UserToken> UserTokens { get; set; } = new List<UserToken>();

    protected User() { }
    public User(
        string email,
        string passwordHash,
        string firstName,
        string lastName)
    {
        Id = Guid.NewGuid();
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
    
    public void UpdatePersonalInfo(string firstName, string lastName, DateOnly? dateOfBirth)
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