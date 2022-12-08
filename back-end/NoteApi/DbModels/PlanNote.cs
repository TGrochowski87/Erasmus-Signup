using System;
using System.Collections.Generic;

namespace NoteApi.DbModels
{
    public partial class PlanNote
    {
        public int NoteId { get; set; }
        public int PlanId { get; set; }
        public string Content { get; set; } = null!;

        public virtual Note Note { get; set; } = null!;
    }
}
