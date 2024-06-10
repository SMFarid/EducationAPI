using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class CourseInstanceHdr
{
    public int CourseInstanceHdrId { get; set; }

    public int? CourseIntId { get; set; }

    public string? CourseCode { get; set; }

    public int? TrackId { get; set; }

    public int? JobProfileIntId { get; set; }

    public int? NumberOfSessions { get; set; }

    public int? NumberOfLabs { get; set; }

    public int? NumberOfExams { get; set; }

    public int? Certification { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpectedEndDate { get; set; }

    public virtual Course? CourseInt { get; set; }

    public virtual JobProfile? JobProfileInt { get; set; }

    public virtual Track? Track { get; set; }
}
