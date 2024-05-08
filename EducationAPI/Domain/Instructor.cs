using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class Instructor
{
    public int Id { get; set; }

    public string SocialId { get; set; } 

    public string NameEn { get; set; } 

    public string? NameAr { get; set; }

    public string Gender { get; set; } 

    public DateOnly Dob { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public string? Email { get; set; }

    public string Mobile { get; set; } 

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

    public virtual ICollection<StudyGroupSession> StudyGroupSessions { get; set; } = new List<StudyGroupSession>();

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
}
