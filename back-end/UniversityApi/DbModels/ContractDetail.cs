using UniversityApi.Models;

namespace UniversityApi.DbModels
{
    public partial class ContractDetail
    {
        public ContractDetail()
        {
            DestSpecialities = new HashSet<DestSpeciality>();
        }

        public short Id { get; set; }
        public bool? AcceptingUndergraduate { get; set; }
        public bool? AcceptingPostgraduate { get; set; }
        public bool? AcceptingDoctoral { get; set; }
        public DateTime? ConclusionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public short? SpecificDetailsId { get; set; }

        public virtual ContractSpecificDetail? SpecificDetails { get; set; }
        public virtual ICollection<DestSpeciality> DestSpecialities { get; set; }
    }
}
