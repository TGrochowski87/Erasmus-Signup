using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionEditVM
    {
        public string? Content { get; set; }
        public short Rating { get; set; }


        public Opinion ToModel(int id)
        {
            return new Opinion()
            {
                Content = Content,
                Rating = Rating,
                Id = id
            };
        }
    }
}
