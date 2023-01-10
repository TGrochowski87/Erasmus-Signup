using NoteApi.DbModels;

namespace NoteApi.Models
{
    public class CommonNoteVM
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;

        public CommonNoteVM(long userId, CommonNotePostVM postVm)
        {
            Id = -1;
            UserId = userId;
            Title = postVm.Title;
            Content = postVm.Content;
        }

        public CommonNoteVM(CommonNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            Title = dbModel.Title;
            Content = dbModel.Content;
        }
    }
}
