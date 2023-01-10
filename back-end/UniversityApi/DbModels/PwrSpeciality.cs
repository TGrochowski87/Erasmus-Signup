using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class PwrSpeciality
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PwrSubject> PwrSubjects { get; } = new List<PwrSubject>();
}
