using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class CollegeSection
{
    public string Id { get; set; } 

    public string NameEn { get; set; } 

    public string? NameAr { get; set; }

    public string? IntId { get; set; }

    public decimal? Capacity { get; set; }

    public string? CollegeId { get; set; }

    public string? CollegeName { get; set; }

    public decimal Active { get; set; }

    public virtual College? College { get; set; }
}
