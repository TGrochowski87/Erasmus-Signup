using System;
using System.Collections.Generic;

namespace UniversityApi.DbModels;

public partial class ContractDetail
{
    public short Id { get; set; }

    public bool? AcceptingUndergraduate { get; set; }

    public bool? AcceptingPostgraduate { get; set; }

    public bool? AcceptingDoctoral { get; set; }

    public short? VacancyMaxPositions { get; set; }

    public short? VacancyMonths { get; set; }

    public DateOnly? ConclusionDate { get; set; }

    public DateOnly? ExpirationDate { get; set; }

    public virtual ICollection<DestSpeciality> DestSpecialities { get; } = new List<DestSpeciality>();
}
