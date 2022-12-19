using NoteApi.DbModels;
using System.Numerics;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityNoteVM
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int SpecialityId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public SpecialityNoteVM(long userId, SpecialityNotePostVM postVm)
        {
            Id = -1;
            UserId = userId;
            SpecialityId = postVm.SpecialityId;
            Title = postVm.Title;
            Content = postVm.Content;
        }

        public SpecialityNoteVM(SpecialityNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            SpecialityId = dbModel.SpecialityId;
            Title = dbModel.Title;
            Content = dbModel.Content;
        }
    }
}
