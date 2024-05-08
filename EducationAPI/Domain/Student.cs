using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class Student
{
    public int StudentIntId { get; set; }
    public string StudentAppId { get; set; }

    public string SocialId { get; set; }

    public string TrackId { get; set; }

    public string? Round_Code { get; set; }

    public string? StudyGovernorate { get; set; }

    public string NameEn { get; set; }

    public string? NameAr { get; set; }

    public string Gender { get; set; }

    public DateOnly Dob { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public string? Nationality { get; set; }

    public string? Email { get; set; }

    public string Mobile { get; set; }

    public string? Phone { get; set; }

    public string? LinkedIn { get; set; }

    public string? Status { get; set; }

    public Boolean? Active { get; set; }

    public virtual StudyGroup? StudyGroup { get; set; }

    public virtual Track? Track { get; set; }
}
