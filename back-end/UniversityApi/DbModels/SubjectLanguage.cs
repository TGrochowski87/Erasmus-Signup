namespace UniversityApi.DbModels
{
    public partial class SubjectLanguage
    {
        public SubjectLanguage()
        {
            DestSpecialities = new HashSet<DestSpeciality>();
        }

        public short Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<DestSpeciality> DestSpecialities { get; set; }
    }
}
