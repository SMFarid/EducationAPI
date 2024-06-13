using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class GroupTrackDet
{
    public int? TrackIntId { get; set; }

    public int? GroupIntId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual StudyGroup? GroupInt { get; set; }

    public virtual Track? TrackInt { get; set; }
}
