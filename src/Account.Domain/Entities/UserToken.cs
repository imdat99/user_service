using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class UserToken : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public TokenType TokenType { get; set; } = TokenType.AccessToken;

    public string TokenHash { get; set; } = null!;

    public string Jti { get; set; } = null!;

    public DateTime ExpiresAt { get; set; }

    public bool? IsRevoked { get; set; }

    public string? DeviceInfo { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public Guid? ParentTokenId { get; set; }

    public DateTime? RevokedAt { get; set; }

    public DateTime? LastUsedAt { get; set; }

    public virtual ICollection<UserToken> InverseParentToken { get; set; } = new List<UserToken>();

    public virtual UserToken? ParentToken { get; set; }

    public virtual User User { get; set; } = null!;
    // Required by EF Core
    protected UserToken() { }
}
public enum TokenType
{
    [System.ComponentModel.DataAnnotations.Display(Name = "access_token")]
    AccessToken,
    [System.ComponentModel.DataAnnotations.Display(Name = "refresh_token")]
    RefreshToken
}