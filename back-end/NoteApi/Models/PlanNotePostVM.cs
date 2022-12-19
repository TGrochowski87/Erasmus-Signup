namespace NoteApi.Models
{
    public class PlanNotePostVM
    {
        public int PlanId { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
