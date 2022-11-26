namespace UniversityApi.DbModels
{
    public partial class PwrSubject
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? SpecialityId { get; set; }
        public int Ects { get; set; }

        public virtual PwrSpeciality? Speciality { get; set; }
    }
}
