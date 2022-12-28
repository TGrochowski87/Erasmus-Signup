namespace UniversityApi.DbModels
{
    public partial class StudyArea
    {
        public StudyArea()
        {
            DestSpecialities = new HashSet<DestSpeciality>();
        }

        public string AreaId { get; set; } = null!;
        public string? AreaName { get; set; }

        public virtual ICollection<DestSpeciality> DestSpecialities { get; set; }
    }
}
