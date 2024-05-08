using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class AuditingSession
{
    public int SessionId { get; set; } 

    public int AuditorId { get; set; } 

    public string? AuditorName { get; set; }

    public DateTime SessionDateTime { get; set; }

    public int CourseId { get; set; }

    public string? CourseName { get; set; }

    public string? ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public int InstructorId { get; set; }

    public string? InstructorName { get; set; }

    public string StudyGroupId { get; set; } 

    public string? SessionType { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public decimal? NumberRegistered { get; set; }

    public decimal? NumberAttended { get; set; }

    public string? CenterId { get; set; }

    public string? CenterName { get; set; }

    public string? AttendanceType { get; set; }

    public decimal? Conducted { get; set; }

    public string? CurrentChapter { get; set; }

    public decimal? CoursePlanWeekNumber { get; set; }

    public decimal? MaterialDelivered { get; set; }

    public decimal? FilesDelivered { get; set; }

    public string? PrevSessionLink { get; set; }

    public string? ConnectionQuality { get; set; }

    public string? VoiceQuality { get; set; }

    public string? VideoQuality { get; set; }

    public byte[]? AttendanceEvidence { get; set; }

    public virtual Auditor Auditor { get; set; } 

    public virtual TrainingCenter? Center { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual TrainingProvider? Provider { get; set; }

    public virtual StudyGroup StudyGroup { get; set; } 
}
