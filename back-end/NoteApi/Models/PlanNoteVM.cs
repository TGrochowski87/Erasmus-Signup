using NoteApi.DbModels;
using System.Text.Json.Serialization;

namespace NoteApi.Models
{
    public class PlanNoteVM
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlanId { get; set; }
        public string Content { get; set; } = null!;

        public PlanNoteVM(int userId, int planId, string content)
        {
            Id = -1;
            UserId = userId;
            PlanId = planId;
            Content = content;
        }

        public PlanNoteVM(PlanNote dbModel)
        {
            Id = dbModel.NoteId;
            UserId = dbModel.Note.UserId;
            CreatedAt = dbModel.Note.CreatedAt;
            PlanId = dbModel.PlanId;
            Content = dbModel.Content;
        }
    }
}
