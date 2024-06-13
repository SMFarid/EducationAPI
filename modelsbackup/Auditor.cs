using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class Auditor
{
    public int Id { get; set; }

    public string? SocialId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public string? Phone { get; set; }

    public string? Mobile { get; set; }

    public string? Email { get; set; }

    public string? LinkedIn { get; set; }

    public bool? Active { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? CreatedBy { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
}
