using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class StudyArea
{
    public string Id { get; set; } = null!;

    public string? AreaName { get; set; }

    public short? StudyDomainId { get; set; }

    public virtual ICollection<DestSpeciality> DestSpecialities { get; } = new List<DestSpeciality>();

    public virtual StudyDomain? StudyDomain { get; set; }
}
