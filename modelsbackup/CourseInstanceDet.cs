using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class CourseInstanceDet
{
    public int CourseInstanceHdrId { get; set; }

    public int? CourseId { get; set; }

    public int? InstructorId { get; set; }

    public int? SrlNo { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public int? ProviderId { get; set; }

    public int? CenterId { get; set; }

    public virtual CourseInstanceHdr CourseInstanceHdr { get; set; } = null!;
}
