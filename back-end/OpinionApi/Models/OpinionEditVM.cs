using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionEditVM
    {
        public float Rating { get; set; }
        public string? Content { get; set; }


        public Opinion ToModel(long Id)
        {
            return new Opinion()
            {
                Content = Content,
                Rating = Rating,
                Id = Id
            };
        }
    }
}
