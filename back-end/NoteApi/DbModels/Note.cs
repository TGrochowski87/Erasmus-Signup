using System;
using System.Collections.Generic;

namespace NoteApi.DbModels
{
    public partial class Note
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual CommonNote? CommonNote { get; set; }
        public virtual PlanNote? PlanNote { get; set; }
        public virtual SpecialityHighlightNote? SpecialityHighlightNote { get; set; }
        public virtual SpecialityNote? SpecialityNote { get; set; }
        public virtual SpecialityPriorityNote? SpecialityPriorityNote { get; set; }
    }
}
