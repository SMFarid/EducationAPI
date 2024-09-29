using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class Auth
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateOnly CreationDate { get; set; }

    public bool Active { get; set; }

    public int Role { get; set; }

    public virtual ICollection<DailyAuditorsAttendance> DailyAuditorsAttendances { get; set; } = new List<DailyAuditorsAttendance>();

    public virtual Role RoleNavigation { get; set; } = null!;

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
    //public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
}
