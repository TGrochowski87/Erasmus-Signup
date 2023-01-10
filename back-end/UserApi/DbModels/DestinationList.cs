using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class DestinationList
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? PreferenceListId { get; set; }

    public int? ListTypeId { get; set; }

    public short DestinationId { get; set; }

    public virtual DestinationListType? ListType { get; set; }

    public virtual DestinationListPreference? PreferenceList { get; set; }

    public virtual AppUser? User { get; set; }
}
