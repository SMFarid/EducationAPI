using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class GroupCourseDet
{
    public int? GroupIntId { get; set; }

    public int? CourseIntId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual Course? CourseInt { get; set; }

    public virtual StudyGroup? GroupInt { get; set; }
}
