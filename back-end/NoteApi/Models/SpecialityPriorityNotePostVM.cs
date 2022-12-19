using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class SpecialityPriorityNotePostVM
    {
        public int SpecialityId { get; set; }
        public short Priority { get; set; }
    }
}
