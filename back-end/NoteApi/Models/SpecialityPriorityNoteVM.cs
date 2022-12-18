using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityPriorityNoteVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public short Priority { get; set; }

        [JsonConstructor]
        public SpecialityPriorityNoteVM(int userId, int specialityId, short priority)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = specialityId;
            Priority = priority;
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
