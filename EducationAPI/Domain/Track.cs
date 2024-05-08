using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class Track
{
    public int Id { get; set; }

    public string Track_Code { get; set; }

    public string NameEn { get; set; }

    public string? NameAr { get; set; }

    public string ProviderId { get; set; } 

    public string? ProviderName { get; set; }

    public decimal? Duration { get; set; }

    public decimal? NumberOfCourses { get; set; }

    public string? Category { get; set; }

    public decimal Active { get; set; }

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<StudyGroup> StudyGroups { get; set; } = new List<StudyGroup>();
}
