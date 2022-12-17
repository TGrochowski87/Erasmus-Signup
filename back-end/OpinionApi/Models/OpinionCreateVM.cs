using OpinionApi.DbModels;

namespace OpinionApi.Models
{
    public class OpinionCreateVM
    {
        public short SpecialityId { get; set; }
        public string? Content { get; set; }
        public short Rating { get; set; }

        public Opinion ToModel(int studentId)
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
