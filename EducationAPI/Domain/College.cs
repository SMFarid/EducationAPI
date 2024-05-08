using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class College
{
    public string Id { get; set; } 

    public string NameEn { get; set; } 

    public string? NameAr { get; set; }

    public string UniversityId { get; set; } 

    public string? IntId { get; set; }

    public string? UniversityName { get; set; }

    public string? Governorate { get; set; }

    public string? Area { get; set; }

    public decimal? NumberOfSections { get; set; }

    public decimal Active { get; set; }

    public virtual ICollection<CollegeSection> CollegeSections { get; set; } = new List<CollegeSection>();

    public virtual University University { get; set; } 
}
