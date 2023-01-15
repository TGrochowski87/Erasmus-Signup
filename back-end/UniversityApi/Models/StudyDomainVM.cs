using Newtonsoft.Json;
using UniversityApi.DbModels;

namespace UniversityApi.Models
{
    
    public class StudyDomainVM
    {
        public short Id { get; set; }

        public string? DomainName { get; set; }

        [JsonConstructor]
        public StudyDomainVM(short id, string? domainName)
        {
            Id = id;
            DomainName = domainName;
        }

        public StudyDomainVM(StudyDomain model)
        {
            Id = model.Id;
            DomainName = model.DomainName;
        }
    }
}
