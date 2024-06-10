using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class College
{
    public int CollegeId { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public int UniversityId { get; set; }

    public string? CollegeCode { get; set; }

    public string? UniversityName { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public int? NumberOfSections { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<CollegeSection> CollegeSections { get; set; } = new List<CollegeSection>();

    public virtual University University { get; set; } = null!;
}
