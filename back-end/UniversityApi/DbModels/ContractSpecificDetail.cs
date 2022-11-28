namespace UniversityApi.DbModels
{
    public partial class ContractSpecificDetail
    {
        public ContractSpecificDetail()
        {
            ContractDetails = new HashSet<ContractDetail>();
        }

        public short Id { get; set; }
        public string? UndergraduateYearRestriction { get; set; }
        public string? PostgraduateYearRestriction { get; set; }
        public string? DoctoralYearRestriction { get; set; }
        public bool? IsAggregate { get; set; }

        public virtual ICollection<ContractDetail> ContractDetails { get; set; }
    }
}
