namespace UniversityApi.DbModels
{
    public partial class PwrSpeciality
    {
        public PwrSpeciality()
        {
            PwrSubjects = new HashSet<PwrSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PwrSubject> PwrSubjects { get; set; }
    }
}
