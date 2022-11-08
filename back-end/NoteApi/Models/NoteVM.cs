using NoteApi.DbModels;

namespace NoteApi.Models
{
    public class NoteVM
    {
        public int Id { get; set; }
        public string? Content { get; set; }

        public NoteVM()
        {

        }

        public NoteVM(Note note)
        {
            Id = note.Id;
            Content = note.Content;
        }
    }
}
