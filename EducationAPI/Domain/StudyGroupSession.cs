using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class StudyGroupSession
{
    public int SessionId { get; set; }

    public string StudyGroupId { get; set; } 

    public decimal MaterialDelivered { get; set; }

    public DateOnly Date { get; set; }

    public string? InstructorName { get; set; }

    public int InstructorId { get; set; }

    public string? ConnectionQuality { get; set; }

    public string? VoiceQuality { get; set; }

    public string? VideoQuality { get; set; }

    public decimal? SessionNumber { get; set; }

    public string? SessionLink { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual StudyGroup StudyGroup { get; set; }
}
