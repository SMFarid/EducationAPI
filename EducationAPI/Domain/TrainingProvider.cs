using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class TrainingProvider
{
    public int ProviderIntId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public decimal? NumberOfTracks { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
}
