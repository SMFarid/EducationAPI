using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class StudyGroup
{
    public string RoundCode { get; set; }

    public string ProviderId { get; set; }

    public int? TrackId { get; set; }

    public int? IntId { get; set; }

    public string? Governorate { get; set; }

    public string? InstructorName { get; set; }

    public int? InstructorId { get; set; }

    public string? StudyGroupType { get; set; }

    public string? CourseName { get; set; }

    public int? CourseId { get; set; }

    public decimal? NumberOfStudents { get; set; }

    public decimal? Capacity { get; set; }

    public string? YearSemester { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();

    public virtual Instructor? Instructor { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<StudyGroupSession> StudyGroupSessions { get; set; } = new List<StudyGroupSession>();

    public virtual Track Track { get; set; }
}
