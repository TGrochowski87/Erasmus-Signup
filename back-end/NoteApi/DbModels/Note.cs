using System.ComponentModel.DataAnnotations.Schema;

namespace NoteApi.DbModels
{
    [Table("Note")]
    public class Note
    {
        public int Id { get; set; }
        public string? Content { get; set; }
    }
}
