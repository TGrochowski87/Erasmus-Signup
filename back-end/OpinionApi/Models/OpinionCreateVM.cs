using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionCreateVM
    {
        public long SpecialityId { get; set; }
        public float Rating { get; set; }
        public string? Content { get; set; }

        public Opinion ToModel(long studentId)
        {
            return new Opinion()
            {
                Content = Content,
                Rating = Rating,
                SpecialityId = SpecialityId,
                StudentId = studentId
            };
        }
    }
}
