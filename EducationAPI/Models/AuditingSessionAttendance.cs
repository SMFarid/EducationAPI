using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class AuditingSessionAttendance
{
    public int? SessionId { get; set; }

    public int? StudentId { get; set; }

    public string? StudentName { get; set; }
    public DateTime? SendDate { get; set; }

    public virtual AuditingSession auditingSession { get; set; }
}
