using System;
using System.Collections.Generic;

namespace EducationAPI.Domain;

public partial class ProviderCenter
{
    public int ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public int CenterId { get; set; }

    public string? CenterName { get; set; }

    public DateTime? ActiveFrom { get; set; }

    public DateTime? ActiveTo { get; set; }

    public virtual TrainingCenter Center { get; set; }

    public virtual TrainingProvider Provider { get; set; }
}
