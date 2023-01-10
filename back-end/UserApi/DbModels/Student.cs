using System;
using System.Collections.Generic;

namespace UserApi.DbModels;

public partial class Student
{
    public int UserId { get; set; }

    public string Index { get; set; } = null!;

    public int PwrSpeciality { get; set; }

    public float AverageGrade { get; set; }

    public short Semester { get; set; }

    public virtual AppUser User { get; set; } = null!;
}
