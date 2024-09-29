using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PicPath { get; set; }

    public string? PicName { get; set; }

    public string? Adress { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Facebook { get; set; }

    public string? Linkedin { get; set; }

    public string? Idnumber { get; set; }

    public virtual Auth User { get; set; } = null!;
}
