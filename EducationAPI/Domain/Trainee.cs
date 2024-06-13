using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class Trainee
{
    public int TraineeIntId { get; set; }

    public string? StudentAppId { get; set; }

    public string? SocialId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public int? TrackId { get; set; }

    public string? RoundCode { get; set; }

    public string? StudyGovernorate { get; set; }

    public string? Gender { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Governorate { get; set; }

    public string? Nationality { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? LinkedIn { get; set; }

    public string? Status { get; set; }

    public bool Active { get; set; }

    public virtual Track? Track { get; set; }
}
