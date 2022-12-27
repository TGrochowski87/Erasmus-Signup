using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class SubjectLanguage
{
    public short Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<DestSpeciality> DestSpecialities { get; } = new List<DestSpeciality>();
}
