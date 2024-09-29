using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class DailyAuditorsAttendance
{
    public int Id { get; set; }

    public int AuditorId { get; set; }

    public DateTime? LoginTime { get; set; }

    public virtual Auth Auditor { get; set; } = null!;
}
