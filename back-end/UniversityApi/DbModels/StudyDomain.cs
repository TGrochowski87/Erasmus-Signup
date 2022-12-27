using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class StudyDomain
{
    public short Id { get; set; }

    public string? DomainName { get; set; }

    public virtual ICollection<StudyArea> StudyAreas { get; } = new List<StudyArea>();
}
