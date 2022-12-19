using NoteApi.DbModels;
using System.Numerics;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityNotePostVM
    {
        public int SpecialityId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
