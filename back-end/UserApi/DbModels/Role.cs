using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class Role
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<AppUser> AppUsers { get; } = new List<AppUser>();
}
