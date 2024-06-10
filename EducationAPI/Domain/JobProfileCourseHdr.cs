using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class JobProfileCourseHdr
{
    public int IntId { get; set; }

    public int? JobProfileIntId { get; set; }

    public int? CourseIntId { get; set; }

    public string? ActiveFrom { get; set; }

    public string? ActiveTo { get; set; }

    public virtual Course? CourseInt { get; set; }

    public virtual JobProfile? JobProfileInt { get; set; }
}
