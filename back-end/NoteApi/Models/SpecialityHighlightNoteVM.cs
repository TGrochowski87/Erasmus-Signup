using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityHighlightNoteVM
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public bool Positive { get; set; }

        public SpecialityHighlightNoteVM(long userId, SpecialityHighlightNotePostVM postVm)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = postVm.SpecialityId;
            Positive = postVm.Positive;
        }

        public SpecialityHighlightNoteVM(SpecialityHighlightNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Positive = dbModel.Positive;
        }
    }
}
