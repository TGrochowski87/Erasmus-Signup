using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityPriorityNoteVM
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public short Priority { get; set; }

        public SpecialityPriorityNoteVM(long userId, SpecialityPriorityNotePostVM postVm)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = postVm.SpecialityId;
            Priority = postVm.Priority;
        }

        public SpecialityPriorityNoteVM(SpecialityPriorityNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Priority = dbModel.Priority;
        }
    }
}
