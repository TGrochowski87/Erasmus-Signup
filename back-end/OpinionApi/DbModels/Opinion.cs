using System;
using System.Collections.Generic;

namespace OpinionApi.DbModels;

public partial class Opinion
{
    public int Id { get; set; }

    public int StudentId { get; set; }

    public short SpecialityId { get; set; }

    public string Content { get; set; } = null!;

    public short Rating { get; set; }
}
