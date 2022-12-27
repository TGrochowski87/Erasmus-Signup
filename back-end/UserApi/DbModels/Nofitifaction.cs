using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class Nofitifaction
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string Content { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual AppUser? User { get; set; }
}
