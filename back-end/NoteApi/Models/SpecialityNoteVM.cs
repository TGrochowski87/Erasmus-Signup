using NoteApi.DbModels;
using System.Numerics;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityNoteVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public string Content { get; set; } = null!;

        [JsonConstructor]
        public SpecialityNoteVM(int userId, int specialityId, string content)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = specialityId;
            Content = content;
        }

        public SpecialityNoteVM(SpecialityNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Content = dbModel.Content;
        }
    }
}
