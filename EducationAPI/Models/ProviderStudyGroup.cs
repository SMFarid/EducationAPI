using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationAPI.Domain;

public partial class ProviderStudyGroup
{
    public int IntId { get; set; }
    public int? ProviderId { get; set; }
    public int? StudyGroupIntId { get; set; }

    public string? Remarks { get; set; }

    public decimal? YearSemester { get; set; }

    public string ProviderName { get; set; }
    public string RoundCode { get; set; }

    public virtual TrainingProvider? Provider { get; set; }
    public virtual StudyGroup? StudyGroup { get; set; }
}
