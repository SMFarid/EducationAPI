using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class Course
{
    public int CourseIntId { get; set; }

    public string CourseCode { get; set; } = null!;

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public decimal? NumberOfSessions { get; set; }

    public decimal? NumberOfLabs { get; set; }

    public decimal? NumberOfExams { get; set; }

    public bool? Certification { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<CourseInstanceHdr> CourseInstanceHdrs { get; set; } = new List<CourseInstanceHdr>();

    public virtual ICollection<GroupSession> GroupSessions { get; set; } = new List<GroupSession>();

    public virtual ICollection<JobProfileCourseHdr> JobProfileCourseHdrs { get; set; } = new List<JobProfileCourseHdr>();
}
