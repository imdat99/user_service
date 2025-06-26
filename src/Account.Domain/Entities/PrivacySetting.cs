using System;
using System.Collections.Generic;
using Account.Shared.Common;

namespace Account.Domain.Entities;

public partial class PrivacySetting : BaseEntity<Guid>
{
    public Guid UserId { get; set; }

    public string? ProfileVisibility { get; set; }

    public bool? ShowEmail { get; set; }

    public bool? ShowPhone { get; set; }

    public bool? ShowBirthDate { get; set; }

    public bool? AllowSearchEngines { get; set; }

    public bool? DataSharingConsent { get; set; }

    public bool? AnalyticsConsent { get; set; }

    public bool? MarketingConsent { get; set; }

    public virtual User User { get; set; } = null!;
}
