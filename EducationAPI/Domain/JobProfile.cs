using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class JobProfile
{
    public int ProfileIntId { get; set; }

    public string? ProfileId { get; set; }

    public int? TrackIntId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public string? ActiveFrom { get; set; }

    public string? ActiveTo { get; set; }

    public virtual ICollection<CourseInstanceHdr> CourseInstanceHdrs { get; set; } = new List<CourseInstanceHdr>();

    public virtual ICollection<JobProfileCourseHdr> JobProfileCourseHdrs { get; set; } = new List<JobProfileCourseHdr>();

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();

    public virtual Track? TrackInt { get; set; }
}
