using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    public class StudyDomainVM
    {
        public short Id { get; set; }

        public string? DomainName { get; set; }


        public StudyDomainVM(StudyDomain model)
        {
            Id = model.Id;
            DomainName = model.DomainName;
        }
    }
}
