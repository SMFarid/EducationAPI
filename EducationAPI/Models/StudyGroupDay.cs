using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class StudyGroupDay
{
    public int StudyGroupId { get; set; }

    public int? SrlNo { get; set; }

    public string? OnlineDay1 { get; set; }

    public string? OnlineDay2 { get; set; }

    public string? OnlineDay3 { get; set; }

    public string? PhysicalDay { get; set; }

    public string? SoftskillDay { get; set; }

    public string? CoachingDay { get; set; }

    public string? EnglishDay { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public string RoundCode { get; set;}

    public TimeSpan? OnlineTimeInterval { get; set; }
    public TimeSpan? PhysicalTimeInterval { get; set; }
    public TimeSpan? SoftskillTimeInterval { get; set; }
    public TimeSpan? CoachingTimeInterval { get; set; }
    public TimeSpan? EnglishTimeInterval { get; set; }

    public virtual StudyGroup studyGroup { get; set; }

    //public virtual StudyGroup StudyGroup { get; set; };
}
