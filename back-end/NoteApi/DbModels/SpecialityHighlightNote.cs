
namespace NoteApi.DbModels
{
    public partial class SpecialityHighlightNote
    {
        public int NoteId { get; set; }
        public int SpecialityId { get; set; }
        public bool Positive { get; set; }

        public virtual Note Note { get; set; } = null!;
    }
}
