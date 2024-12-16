using EducationAPI.Domain;
using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class AuditingSession
{
    public AuditingSession()
    {
        AuditingSessionAttendances = new List<AuditingSessionAttendance>();
    }

    public int SessionId { get; set; }

    public int AuditorId { get; set; }

    public string? AuditorName { get; set; }

    public DateTime? SessionDateTimeStart { get; set; }

    public DateTime? SessionDateTimeClose { get; set; }

    public string? CourseId { get; set; }

    public string? CourseName { get; set; }

    public int? ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public int? InstructorId { get; set; }

    public string? InstructorName { get; set; }

    public string StudyGroupId { get; set; } = null!;

    public string? SessionType { get; set; }

    public string? AttendanceType { get; set; }

    public bool? LabFlag { get; set; }

    public bool? TestFlag { get; set; }

    public TimeOnly? StartTime { get; set; }

    public TimeOnly? EndTime { get; set; }

    public decimal? NumberRegistered { get; set; }

    public decimal? NumberAttended { get; set; }

    public int? CenterId { get; set; }

    public string? CenterName { get; set; }

    public bool? Conducted { get; set; }

    public string? CurrentChapter { get; set; }

    public decimal? PlannedChapter { get; set; }

    public bool? MaterialDelivered { get; set; }

    public string? PrevSessionLink { get; set; }

    public string? ConnectionQuality { get; set; }

    public string? VoiceQuality { get; set; }

    public string? VideoQuality { get; set; }

    public byte[]? AttendanceEvidence { get; set; }

    public string? InstructorResponsiveness { get; set; }

    public bool? SessionLinkDelivered { get; set; }

    public bool? DepiLogoAdded { get; set; }

    public bool? PresentationUsed { get; set; }

    public string? HardwareProficiency { get; set; }
    public bool? UnderstoodExamples {  get; set; }
    public bool? UnderstoonExplaination { get; set; }
    public bool? TimeForQuestions { get; set; }
    public bool? InstructorEncouragement { get; set; }
    public bool? MaterialIsClear {  get; set; }
    public string? ACCondition { get; set; }
    public bool? CenterEnvironment { get; set; }
    public bool? InitiativeClear { get; set; }
    public bool? PrevLinks {  get; set; }
    public string? CommentCategory { get; set; }
    public int? AssignmentSessionID { get; set; }
    public string? Remarks { get; set; }
    public DateTime? LastModified { get; set; }

    //Added 29/11/2024
    public bool? IsCameraOpen { get; set; }
    public bool? IsLastSessionExam { get; set; }
    public string? InstructorPicturePath {  get; set; }


    //public virtual AuditorRoundCodeAssignment auditorRoundCodeAssignment { get; set; }

    //public virtual Auditor Auditor { get; set; } = null!;

    public virtual TrainingCenter? Center { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual TrainingProvider? Provider { get; set; }

    public virtual ICollection<AuditingSessionAttendance>? AuditingSessionAttendances { get; set; }
}
