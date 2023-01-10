using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionGetVM
    {
        public long Id { get; set; }
        //public long StudentId { get; set; }
        public double Rating { get; set; }
        public string Content { get; set; } = null!;
        public bool CanEdit { get; set; }


        public OpinionGetVM(Opinion model, long? userId)
        {
            Id = model.Id;
            //StudentId = model.StudentId;
            Content = model.Content;
            Rating = model.Rating;
            CanEdit = userId == model.StudentId;
        }
    }
}
