using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class TrainingCenter
{
    public string Id { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public decimal? NumberOfLabs { get; set; }

    public decimal? NumberOfClassrooms { get; set; }

    public decimal? Active { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
    public virtual ICollection<ProviderCenter> ProviderCenters { get; set; }
}
