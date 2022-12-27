using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class DestinationListPreference
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DestinationList> DestinationLists { get; } = new List<DestinationList>();
}
