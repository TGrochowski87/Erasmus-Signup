using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class UserProfile
{
    public short Id { get; set; }

    public int UserId { get; set; }

    public double AverageGrade { get; set; }

    public short? PreferencedStudyDomainId { get; set; }
}
