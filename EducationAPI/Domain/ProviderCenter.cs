using System;
using System.Collections.Generic;

namespace EducationAPI.Models;

public partial class ProviderCenter
{
    public string ProviderId { get; set; }

    public string? ProviderName { get; set; }

    public string CenterId { get; set; }

    public string? CenterName { get; set; }

    public virtual TrainingCenter Center { get; set; }

    public virtual TrainingProvider Provider { get; set; }
}
