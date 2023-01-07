using System;
using System.Collections.Generic;

namespace PlanApi.DbModels;

public partial class Plan
{
    public int Id { get; set; }

    public long StudentId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Subject> Subjects { get; } = new List<Subject>();
}
