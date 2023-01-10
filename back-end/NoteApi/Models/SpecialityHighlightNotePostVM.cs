using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityHighlightNotePostVM
    {
        public int SpecialityId { get; set; }
        public bool Positive { get; set; }
    }
}
