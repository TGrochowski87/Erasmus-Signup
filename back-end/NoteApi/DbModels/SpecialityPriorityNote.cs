using System;
using System.Collections.Generic;

namespace NoteApi.DbModels
{
    public partial class SpecialityPriorityNote
    {
        public int NoteId { get; set; }
        public int SpecialityId { get; set; }
        public short Priority { get; set; }

        public virtual Note Note { get; set; } = null!;
    }
}
