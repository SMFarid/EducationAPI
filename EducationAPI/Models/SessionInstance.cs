using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class SessionInstance
{
    public int SessionId { get; set; }

    public int? SrlNo { get; set; }

    public DateTime? SessionDate { get; set; }

    public string? SessionRecording { get; set; }

    public int? GroupIntId { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    //public virtual StudyGroup? GroupInt { get; set; }

    public virtual GroupSession Session { get; set; } = null!;
}
