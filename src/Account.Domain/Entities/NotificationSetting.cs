using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class NotificationSetting : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public bool? EmailNotifications { get; set; }

    public bool? SmsNotifications { get; set; }

    public bool? PushNotifications { get; set; }

    public bool? MarketingEmails { get; set; }

    public bool? SecurityAlerts { get; set; }

    public bool? LoginAlerts { get; set; }

    public bool? ProfileUpdates { get; set; }

    public bool? PaymentNotifications { get; set; }

    public virtual User User { get; set; } = null!;
}
