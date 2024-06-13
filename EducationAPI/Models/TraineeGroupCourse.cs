using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class TraineeGroupCourse
{
    public int TraineeId { get; set; }

    public int GroupIntId { get; set; }

    public string? RoundCode { get; set; }

    public int? SrlNo { get; set; }

    public bool? Completed { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public int? CourseId { get; set; }

    public virtual CourseInstanceHdr? Course { get; set; }

    public virtual StudyGroup GroupInt { get; set; } = null!;

    public virtual Trainee Trainee { get; set; } = null!;
}
