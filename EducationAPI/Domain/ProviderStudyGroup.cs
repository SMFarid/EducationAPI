using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class ProviderStudyGroup
{
    public int? ProviderId { get; set; }

    public int? StudyGroupIntId { get; set; }

    public string? Remarks { get; set; }

    public decimal? YearSemester { get; set; }

    public virtual TrainingProvider? Provider { get; set; }

    public virtual StudyGroup? StudyGroupInt { get; set; }
}
