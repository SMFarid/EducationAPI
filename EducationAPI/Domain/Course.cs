using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class Course
{
    public int CourseId { get; set; } 

    public int IntId { get; set; }

    public string TrackId { get; set; } 

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? TrackName { get; set; }

    public decimal? NumberOfSessions { get; set; }

    public decimal? NumberOfLabs { get; set; }

    public decimal? NumberOfExams { get; set; }

    public decimal? Certification { get; set; }

    public string? InstructorName { get; set; }

    public string? InstructorId { get; set; }

    public decimal? Capacity { get; set; }

    public decimal? OnlineOfflineFlag { get; set; }

    public decimal? NumberOfStudyGroups { get; set; }

    public decimal Active { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();
}
