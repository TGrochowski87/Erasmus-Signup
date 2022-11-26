namespace UniversityApi.DbModels
{
    public partial class StudyArea
    {
        public StudyArea()
        {
            DestSpecialities = new HashSet<DestSpeciality>();
        }

        public string StudyDomain { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<DestSpeciality> DestSpecialities { get; set; }
    }
}
