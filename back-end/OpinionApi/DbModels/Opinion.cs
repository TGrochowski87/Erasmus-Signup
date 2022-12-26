using System;
using System.Collections.Generic;

namespace OpinionApi.DbModels;

public partial class Opinion
{
    public long Id { get; set; }
    public long SpecialityId { get; set; }
    public long StudentId { get; set; }
    public string Content { get; set; } = null!;
    public double Rating { get; set; }

}
