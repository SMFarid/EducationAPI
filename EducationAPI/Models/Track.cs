using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class Track
{
    public int TrackIntId { get; set; }

    public string? TrackCode { get; set; }

    public string? TrackNameEn { get; set; }

    public string? TrackNameAr { get; set; }

    public virtual ICollection<CourseInstanceHdr> CourseInstanceHdrs { get; set; } = new List<CourseInstanceHdr>();

    public virtual ICollection<JobProfile> JobProfiles { get; set; } = new List<JobProfile>();

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();

    public virtual ICollection<TrackProvider> TrackProviders { get; set; } = new List<TrackProvider>();

    public virtual ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
}
