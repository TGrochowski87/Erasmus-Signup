using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class University
{
    public string ErasmusCode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Country { get; set; }

    public string? City { get; set; }

    public string? Email { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<DestSpeciality> DestSpecialities { get; } = new List<DestSpeciality>();
}
