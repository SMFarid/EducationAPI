using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class GroupSession
{
    public int SessionId { get; set; }

    public int? GroupIntId { get; set; }

    public DateTime? Date { get; set; }

    public bool? SessionStatus { get; set; }

    public int? StudyGroupIntId { get; set; }

    public int? SrlNo { get; set; }

    public TimeOnly? SessionTime { get; set; }

    public int? InstructorId { get; set; }

    public int? CourseIntId { get; set; }

    public virtual Course? CourseInt { get; set; }

    public virtual Instructor? Instructor { get; set; }

    public virtual StudyGroup? StudyGroupInt { get; set; }
}
