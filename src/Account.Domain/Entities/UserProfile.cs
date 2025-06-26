using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class UserProfile : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string? Bio { get; set; }

    public string? Website { get; set; }

    public string? Location { get; set; }

    public string? Timezone { get; set; }

    public string? Language { get; set; }

    public Gender? Gender { get; set; }

    public ProfileVisibility? ProfileVisibility { get; set; }

    public virtual User User { get; set; } = null!;
    // Navigation property

    // Required by EF Core
    protected UserProfile() { }

    public UserProfile(Guid userId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateProfile(string? bio, string? website, string? location)
    {
        Bio = bio;
        Website = website;
        Location = location;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdatePreferences(string timezone, string language, Gender? gender)
    {
        Timezone = timezone;
        Language = language;
        Gender = gender;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateVisibility(ProfileVisibility visibility)
    {
        ProfileVisibility = visibility;
        UpdatedAt = DateTime.UtcNow;
    }
}
public enum Gender
{
    Male,
    Female,
    Other,
    PreferNotToSay
}

public enum ProfileVisibility
{
    Public,
    FriendsOnly,
    Private
}
