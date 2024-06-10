using System;
using System.Collections.Generic;

namespace EducationAPI;

public partial class University
{
    public int UniversityId { get; set; }

    public string NameEn { get; set; } = null!;

    public string? NameAr { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public string? Address { get; set; }

    public string? Type { get; set; }

    public int? NumberOfColleges { get; set; }

    public bool Active { get; set; }

    public virtual ICollection<College> Colleges { get; set; } = new List<College>();
}
