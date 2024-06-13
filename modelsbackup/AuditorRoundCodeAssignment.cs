using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class AuditorRoundCodeAssignment
{
    public int AuditorId { get; set; }

    public string StudyGroupRoundCode { get; set; } = null!;

    public DateOnly? Date { get; set; }

    public bool? Conducted { get; set; }

    public int? AuditingSessionId { get; set; }

    public virtual Auditor Auditor { get; set; } = null!;
}
