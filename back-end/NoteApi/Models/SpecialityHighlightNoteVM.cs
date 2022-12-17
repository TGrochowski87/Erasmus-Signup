using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityHighlightNoteVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public bool Positive { get; set; }

        [JsonConstructor]
        public SpecialityHighlightNoteVM(int userId, int specialityId, bool positive)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = specialityId;
            Positive = positive;
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
