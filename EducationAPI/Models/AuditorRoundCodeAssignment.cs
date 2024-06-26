using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class AuditorRoundCodeAssignment
{
    public int? AuditorId { get; set; }

    public string StudyGroupRoundCode { get; set; }

    public DateTime? Date { get; set; }

    public int? Conducted { get; set; }

    public int AssignmentSessionID { get; set; }

    public virtual Auditor Auditor { get; set; } = null!;
}
