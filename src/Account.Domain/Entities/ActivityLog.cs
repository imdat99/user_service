using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class ActivityLog : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public ActivityType ActivityType { get; set; } = ActivityType.Login;

    public string? Description { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public string? Metadata { get; set; }

    public virtual User User { get; set; } = null!;
    // Required by EF Core
    protected ActivityLog() { }
}
public enum ActivityType
{
    [System.ComponentModel.DataAnnotations.Display(Name = "login")]
    Login,
    [System.ComponentModel.DataAnnotations.Display(Name = "logout")]
    Logout ,
    [System.ComponentModel.DataAnnotations.Display(Name = "profile_update")]
    Profileupdate ,
    [System.ComponentModel.DataAnnotations.Display(Name = "password_change")]
    Passwordchange ,
    [System.ComponentModel.DataAnnotations.Display(Name = "email_change")]
    Emailchange,
    [System.ComponentModel.DataAnnotations.Display(Name = "phone_change")]
    Phonechange ,
    [System.ComponentModel.DataAnnotations.Display(Name = "2fa_enable")]
    TwofaEnable,
    [System.ComponentModel.DataAnnotations.Display(Name = "2fa_disable")]
    TwofaDisable ,
    [System.ComponentModel.DataAnnotations.Display(Name = "payment_add")]
    Paymentadd ,
    [System.ComponentModel.DataAnnotations.Display(Name = "payment_remove")]
    Paymentremove ,
    [System.ComponentModel.DataAnnotations.Display(Name = "transaction")]
    Transaction,
}