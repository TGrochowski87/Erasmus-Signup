using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class Coordinator
{
    public int UserId { get; set; }

    public int PwrSpeciality { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
