using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class University
{
    public string Id { get; set; } 

    public string NameEn { get; set; } 

    public string? NameAr { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public string? Address { get; set; }

    public string? Type { get; set; }

    public decimal? NumberOfColleges { get; set; }

    public decimal Active { get; set; }

    public virtual ICollection<College> Colleges { get; set; } = new List<College>();
}
