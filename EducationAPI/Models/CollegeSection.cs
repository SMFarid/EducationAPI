using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class CollegeSection
{
    public int Id { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? SectionCode { get; set; }

    public int? Capacity { get; set; }

    public int? CollegeId { get; set; }

    public string? CollegeName { get; set; }

    public bool Active { get; set; }

    public virtual College? College { get; set; }
}
