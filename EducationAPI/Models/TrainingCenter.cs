using EducationAPI.Models;
using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class TrainingCenter
{
    public int CenterIntId { get; set; }

    public string? CenterCode { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public decimal? NumberOfLabs { get; set; }

    public decimal? NumberOfClassrooms { get; set; }

    public bool? Active { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
}
