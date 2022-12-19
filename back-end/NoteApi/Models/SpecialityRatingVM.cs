using NoteApi.DbModels;

namespace NoteApi.Models
{
    public class SpecialityRatingVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public short Priority { get; set; }
        public bool? Positive { get; set; }

        public SpecialityRatingVM(SpecialityPriorityNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Priority = dbModel.Priority;
            Positive = null;
        }

        public SpecialityRatingVM(SpecialityHighlightNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Priority = 0;
            Positive = dbModel.Positive;
        }
    }
}
