using System;
using System.Collections.Generic;

namespace NoteApi.DbModels
{
    public partial class SpecialityNote
    {
        public int NoteId { get; set; }
        public int SpecialityId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public virtual Note Note { get; set; } = null!;
    }
}
