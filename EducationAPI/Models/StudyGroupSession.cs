using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class StudyGroupSession
{
    public int SessionId { get; set; }

    public int StudyGroupId { get; set; }

    public bool MaterialDelivered { get; set; }

    public DateTime Date { get; set; }

    public string? InstructorName { get; set; }

    public int? InstructorId { get; set; }

    public int? ConnectionQuality { get; set; }

    public int? VoiceQuality { get; set; }

    public int? VideoQuality { get; set; }

    public int? SessionNumber { get; set; }

    public string? SessionLink { get; set; }

    public TimeOnly? TimeInterval { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual StudyGroup StudyGroup { get; set; } = null!;
}
