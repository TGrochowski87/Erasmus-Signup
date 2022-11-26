namespace UniversityApi.DbModels
{
    public partial class MinGradeHistory
    {
        public int Id { get; set; }
        public short? DestSpecialityId { get; set; }
        public float? Grade { get; set; }
        public string? Semester { get; set; }

        public virtual DestSpeciality? DestSpeciality { get; set; }
    }
}
