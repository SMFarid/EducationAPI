using EducationAPI.Models;
using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class Instructor
{
    public int InstructorIntId { get; set; }

    public string SocialId { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string Gender { get; set; } = null!;

    public DateOnly Dob { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public string? Email { get; set; }

    public string Mobile { get; set; } = null!;

    public string? Phone { get; set; }

    public string? LinkedIn { get; set; }

    public string? UniversityName { get; set; }

    public string? CollegeName { get; set; }

    public string? UniversityId { get; set; }

    public string? CollegeId { get; set; }

    public string? Certificate { get; set; }

    public string? Grade { get; set; }

    public DateOnly? GraduationDate { get; set; }

    public decimal Active { get; set; }

    public virtual ICollection<AuditingSession> AuditingSessions { get; set; } = new List<AuditingSession>();

    public virtual ICollection<GroupSession> GroupSessions { get; set; } = new List<GroupSession>();

    public virtual ICollection<StudyGroupSession> StudyGroupSessions { get; set; } = new List<StudyGroupSession>();

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
}
