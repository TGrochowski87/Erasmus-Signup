using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionGetVM
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public string Content { get; set; } = null!;

        public short Rating { get; set; }
        public bool CanEdit { get; set; }


        public OpinionGetVM(Opinion model, int? userId)
        {
            Id = model.Id;
            StudentId = model.StudentId;
            Content = model.Content;
            Rating = model.Rating;
            CanEdit = userId == model.StudentId;
        }
    }
}
