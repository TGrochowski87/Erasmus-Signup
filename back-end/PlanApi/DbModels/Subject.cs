using System;
using System.Collections.Generic;

namespace PlanApi.DbModels;

public partial class Subject
{
    public int Id { get; set; }

    public int? PlanId { get; set; }

    public int? MappedSubject { get; set; }

    public string Name { get; set; } = null!;

    public short Ects { get; set; }

    public virtual HomeSubject? MappedSubjectNavigation { get; set; }

    public virtual Plan? Plan { get; set; }
}
