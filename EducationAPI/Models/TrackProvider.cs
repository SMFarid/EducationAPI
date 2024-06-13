using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class TrackProvider
{
    public int IntId { get; set; }

    public int? TrackIntId { get; set; }

    public string? NameEn { get; set; }

    public string? NameAr { get; set; }

    public int? ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public int? TrackDuration { get; set; }

    public int? NumberOfCourses { get; set; }

    public int? Category { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public string? TrackLeader { get; set; }

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();

    public virtual Track? TrackInt { get; set; }
}
