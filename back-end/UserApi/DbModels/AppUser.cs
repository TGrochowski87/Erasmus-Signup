using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class AppUser
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public short? RoleId { get; set; }

    public string Email { get; set; } = null!;

    public virtual Coordinator? Coordinator { get; set; }

    public virtual ICollection<DestinationList> DestinationLists { get; } = new List<DestinationList>();

    public virtual ICollection<Nofitifaction> Nofitifactions { get; } = new List<Nofitifaction>();

    public virtual Role? Role { get; set; }

    public virtual Student? Student { get; set; }
}
